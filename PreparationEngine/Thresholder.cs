using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace PreparationEngine
{
    internal class Thresholder
    {
        private readonly Size _elementSize = new Size(3, 13);

        public Mat Process(Mat input)
        {
            var output = input.Sobel(MatType.CV_8U, 1, 0).Threshold(0, 255, ThresholdType.Otsu | ThresholdType.Binary);
            var element = Cv2.GetStructuringElement(StructuringElementShape.Rect, _elementSize);
            return output.MorphologyEx(MorphologyOperation.Close, element);
        }
    }
}
