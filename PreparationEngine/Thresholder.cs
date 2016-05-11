using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace PreparationEngine
{
    internal class Thresholder
    {
        private readonly Size _elementSizeEllipse = new Size(3, 3);
        private readonly Size _elementSizeRect = new Size(1, 9);

        public Mat Process(Mat input)
        {
            var output = input;
            var element = Cv2.GetStructuringElement(StructuringElementShape.Ellipse, _elementSizeEllipse);
            output = output.MorphologyEx(MorphologyOperation.Gradient, element);
            output = output.Threshold(0, 255, ThresholdType.Binary | ThresholdType.Otsu);
            element = Cv2.GetStructuringElement(StructuringElementShape.Rect, _elementSizeRect);
            return output.MorphologyEx(MorphologyOperation.Close, element);
        }
    }
}
