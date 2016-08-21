using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlPipeline;
using OpenCvSharp.CPlusPlus;

namespace RecognitionEngine
{
    public static class DataConverter
    {
        public static bool[] DigitToBrainOutput(int digit)
        {
            if (digit >= 0 && digit <= 9)
            {
                var output = Enumerable.Repeat(false, 16).ToArray();
                output[digit] = true;
                return output;
            }
            ErrorPipe.Instance.ErrorMessage(RecognitionController.Instance, "Liczba podana do DataConverter wychodzi poza zakres [0, 9].");
            return null;
        }

        public static string BrainOutputToDigit(IEnumerable<bool> output)
        {
            var digit = output.ToList().IndexOf(true);
            if (digit < 0 || digit > 9)
            {
                ErrorPipe.Instance.ErrorMessage(RecognitionController.Instance, "Błąd sieci! Rozpoznana cyfra jest poza zakresem [0, 9].");
            }
            return digit.ToString();
        }

        public static bool[] BrainInputFromBitmap(Bitmap bitmap)
        {
            var result = new bool[bitmap.Height*bitmap.Width];
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    result[(bitmap.Width * y) + x] = GetPixelValue(bitmap.GetPixel(x, y));
                }
            }
            return result;
        }

        public static bool[] BrainInputFromMat(Mat mat)
        {
            var byte3Col = new MatOfByte3(mat);
            var indexer = byte3Col.GetIndexer();
            var pixel = indexer[0, 1];

            var result = new bool[mat.Height*mat.Width];
            for (int x = 0; x < mat.Width; x++)
            {
                for (int y = 0; y < mat.Height; y++)
                {
                    result[(mat.Width*y) + x] = GetIndexerValue(indexer[y, x]);
                }
            }
            return result;
        }

        public static string BoolArrayToString(bool[] input)
        {
            var builder = new StringBuilder();
            foreach (bool value in input)
            {
                builder.Append(value ? "t" : "f");
            }
            return builder.ToString();
        }

        public static bool[] StringToBoolArray(string input)
        {
            return input.Select(c => c == 't').ToArray();
        }

        private static bool GetPixelValue(Color pixel)
        {
            var value = (pixel.R + pixel.G + pixel.B);
            return value >= 382;
        }

        private static bool GetIndexerValue(Vec3b pixel)
        {
            var value = (pixel.Item0 + pixel.Item1 + pixel.Item2);
            return value >= 382;
        }
    }
}
