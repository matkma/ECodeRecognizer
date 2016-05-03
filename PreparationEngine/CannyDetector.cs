using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace PreparationEngine
{
    internal class CannyDetector
    {
        private const int Threshold = 20;
        private const int Ratio = 3;

        public Mat Detect(Mat input)
        {
            var canny = input.Blur(new Size(3, 3)).Canny(Threshold, Threshold*Ratio);
            var output = new Mat(canny.Size(), canny.Type(), Scalar.All(0));
            input.CopyTo(output, canny);
            return output;
        }
    }
}
