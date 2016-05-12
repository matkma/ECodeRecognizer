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
        private readonly double _lastMaxFill = 0.20;

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
            var thirdCheck = false;
            var thirdCol = 0;

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
                        secondCheck = CheckSecondCondition(col, 11);
                    }
                    else
                    {
                        thirdCheck = CheckThirdCondition(col);
                        if (thirdCheck)
                        {
                            using (new Window(GetType().ToString() + " (wciśnij ENTER jeśli obraz jest poprawny)", input))
                            {
                                Cv2.WaitKey();
                            }
                            thirdCol = i;
                            break;
                        }
                    }
                }
            }
            if (thirdCol != 0)
            {
                var index = (double)(thirdCol + input.Cols) / 2;
                var col = input.Col[(int)Math.Floor(index)];
                return CheckLastCondition(col);
            }
            return false;
        }

        private bool CheckFirstCondition(Mat col, double minFill)
        {
            var fill = (double)Cv2.CountNonZero(col) / (double)col.Height;
            return fill >= minFill;
        }

        private bool CheckSecondCondition(Mat col, int checkCount)
        {
            var checks = new bool[checkCount];
            for (var i = 0; i < checkCount; i++)
            {
                checks[i] = false;
            }
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

        private bool CheckThirdCondition(Mat col)
        {
            //TODO
            //Nie działa, bo trzy łapki E nie kończą się w tym samym miejscu (no przecież)
            //Znaleźć inny ostatni warunek
            return CheckSecondCondition(col, 5);
        }

        private bool CheckLastCondition(Mat col)
        {
            return !CheckFirstCondition(col, _lastMaxFill);
        }

        private bool CheckPixel(Vec3b pixel)
        {
            return pixel.Item0 + pixel.Item1 + pixel.Item2 >= 2*255;
        }
    }
}
