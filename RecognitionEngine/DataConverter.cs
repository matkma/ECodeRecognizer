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
        public static double[] DigitToNetworkOutput(int digit)
        {
            if (digit >= 0 && digit <= 9)
            {
                var output = Enumerable.Repeat(0.0, 16).ToArray();
                output[digit] = 1;
                return output;
            }
            ErrorPipe.Instance.ErrorMessage(NeuralNetworkController.Instance, "Liczba podana do DataConverter wychodzi poza zakres [0, 9].");
            return null;
        }

        public static string NetworkOutputToDigit(IEnumerable<double> output)
        {
            var digit = output.ToList().IndexOf(output.Max());
            if (digit < 0 || digit > 9)
            {
                ErrorPipe.Instance.ErrorMessage(NeuralNetworkController.Instance, "Błąd sieci! Rozpoznana cyfra jest poza zakresem [0, 9].");
            }
            return digit.ToString();
        }

        public static double[] NetworkInputFromBitmap(Bitmap bitmap)
        {
            var result = new double[bitmap.Height*bitmap.Width];
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    result[(bitmap.Width * y) + x] = GetPixelValue(bitmap.GetPixel(x, y));
                }
            }
            return result;
        }

        public static double[] NetworkInputFromMat(Mat mat)
        {
            var byte3Col = new MatOfByte3(mat);
            var indexer = byte3Col.GetIndexer();
            var pixel = indexer[0, 1];

            var result = new double[mat.Height*mat.Width];
            for (int x = 0; x < mat.Width; x++)
            {
                for (int y = 0; y < mat.Height; y++)
                {
                    result[(mat.Width*y) + x] = GetIndexerValue(indexer[y, x]);
                }
            }
            return result;
        }

        private static double GetPixelValue(Color pixel)
        {
            var value = (pixel.R + pixel.G + pixel.B);
            return value >= 382 ? 1 : 0;
        }

        private static double GetIndexerValue(Vec3b pixel)
        {
            var value = (pixel.Item0 + pixel.Item1 + pixel.Item2);
            return value >= 382 ? 1 : 0;
        }
    }
}
