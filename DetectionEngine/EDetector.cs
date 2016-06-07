using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace DetectionEngine
{
    public class EDetector
    {
        private readonly double _armMinFill = 0.70;
        private readonly double _notArmMaxFill = 0.50;
        private readonly double _emptyMaxFill = 0.25;

        public List<Rect> DetectCapitalE(Mat input, List<Rect> letters)
        {
            return (from letter 
                    in letters 
                    let mask = new Mat(input, letter) 
                    where FindCapitalE(mask) 
                    select letter)
                    .ToList();
        }

        private bool FindCapitalE(Mat input)
        {
            bool[] checks = {false, false, false, false, false, false};
            int? testColumnIndex = null;
            var testPixel = false;
            var checkIndex = 0;

            for (var i = 0; i < input.Rows; i++)
            {
                if (checkIndex == checks.Length) return true;
                if (testColumnIndex != null)
                {
                    if (!CheckPixel(new MatOfByte3(input.Row[i]).GetIndexer()[0, testColumnIndex.Value]))
                    {
                        if (testPixel) return false;
                        else
                        {
                            testPixel = true;
                        }
                    }      
                }       
                if (checkIndex%2 == 0)
                {
                    checks[checkIndex] = IsArm(input.Row[i]);
                    if (checkIndex == 4 && testColumnIndex != null) testColumnIndex = null;
                }
                else if (checkIndex == checks.Length - 1)
                {
                    checks[checkIndex] = IsEmpty(input.Row[i]);
                }
                else
                {
                    checks[checkIndex] = IsNotArm(input.Row[i], ref testColumnIndex);
                }

                if (checks[checkIndex])
                {
                    checkIndex++;
                }
            }

            return checks.Last() || checks[4];
        }

        private bool IsArm(Mat row)
        {
            var fill = (double)Cv2.CountNonZero(row) / (double)row.Width;
            return fill >= _armMinFill;
        }

        private bool IsNotArm(Mat row, ref int? index)
        {
            var fillCheck = false;
            var shapeCheck = false;
            var fill = (double)Cv2.CountNonZero(row) / (double)row.Width;
            fillCheck = fill <= _notArmMaxFill;
            if (fillCheck)
            {
                var byte3Col = new MatOfByte3(row);
                var indexer = byte3Col.GetIndexer();
                bool start = false, end = false, buffer = false;
                for (var col = 0; col < row.Cols; col++)
                {
                    var pixel = indexer[0, col];
                    if (!start)
                    {
                        if (!CheckPixel(pixel)) continue;
                        start = true;
                        if (index == null)
                        {
                            index = col + 2;
                        }
                    }
                    else if (!end)
                    {
                        if (!buffer)
                        {
                            if (CheckPixel(pixel)) continue;
                            buffer = true;
                            if (index == col)
                            {
                                index = col - 1;
                            }
                        }
                        else
                        {
                            if (!CheckPixel(pixel)) end = true;
                        }
                    }
                    else
                    {
                        if (CheckPixel(pixel)) end = false;
                    }
                }

                shapeCheck = start && end;
            }
            return shapeCheck && fillCheck;
        }

        private bool IsEmpty(Mat row)
        {
            var fill = (double)Cv2.CountNonZero(row) / (double)row.Width;
            return fill <= _emptyMaxFill;
        }

        private bool CheckPixel(Vec3b pixel)
        {
            return pixel.Item0 + pixel.Item1 + pixel.Item2 >= 2*255;
        }
    }
}
