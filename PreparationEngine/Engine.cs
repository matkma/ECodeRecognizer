using System.Drawing;
using ControlPipeline;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace PreparationEngine
{
    public class Engine
    {
        private DetectionEngine.Engine detectionEngine = new DetectionEngine.Engine();

        public void Input(Bitmap bmp)
        {
            ProgressPipe.Instance.ProgressMessage("Przygotowywanie obrazu do dalszego przetwarzania...");
            var image = OpenCvSharp.Extensions.BitmapConverter.ToMat(bmp);
            Process(image);
        }

        private void Process(Mat input)
        {
            var output = input.CvtColor(ColorConversion.BgrToGray).Threshold(128, 255, ThresholdType.Binary);
            detectionEngine.Input(output);
        }
    }
}
