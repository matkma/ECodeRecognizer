using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace DetectionEngine
{
    public class EDetector
    {
        private readonly double _firstMinFill = 0.50;
        private readonly double _lastMinFill = 0.33;

        public List<Rect> DetectCapitalE(Mat input, List<Rect> letters)
        {
            input = input.Threshold(30, 255, ThresholdType.Binary);
            return (from letter 
                    in letters 
                    let mask = new Mat(input, letter) 
                    where FindCapitalE(mask) 
                    select letter)
                    .ToList();
        }

        private bool FindCapitalE(Mat input)
        {
            var firstCheck = false;
            var secondCheck = false;

            for (var i = 0; i < input.Cols; i++)
            {
                var col = input.Col[i];
                if (!firstCheck)
                {
                    firstCheck = CheckFirstCondition(col, _firstMinFill);
                }
                else
                {
                    if (!secondCheck)
                    {
                        secondCheck = CheckSecondCondition(col);
                    }
                    else
                    {
                        var thirdCheck = CheckFirstCondition(col, _lastMinFill);
                        if (thirdCheck)
                        {
                            return false;
                        }
                    }
                }
            }
            return secondCheck;
        }

        private bool CheckFirstCondition(Mat col, double minFill)
        {
            var fill = (double)Cv2.CountNonZero(col) / (double)col.Height;
            return fill >= minFill;
        }

        private bool CheckSecondCondition(Mat col)
        {
            bool[] checks = {false, false, false, false, false, false, false, false, false, false, false};
            var step = 0;
            var byte3Col = new MatOfByte3(col);
            var indexer = byte3Col.GetIndexer();
            for (var row = 0; row < col.Rows; row++)
            {
                var pixel = indexer[row, 0];
                if (step % 2 == 0)
                {
                    if (!CheckPixel(pixel)) continue;
                    checks[step] = true;
                    step++;
                }
                else
                {
                    if (CheckPixel(pixel)) continue;
                    checks[step] = true;
                    step++;
                }
                if (checks.Last())
                {
                    return true;
                }
            }
            return checks.All(check => check);
        }

        private bool CheckPixel(Vec3b pixel)
        {
            return pixel.Item0 + pixel.Item1 + pixel.Item2 == 3*255;
        }
    }
}
