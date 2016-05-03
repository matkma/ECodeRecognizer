using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using ControlPipeline;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace PreparationEngine
{
    public class Engine
    {
        private readonly DetectionEngine.Engine _detectionEngine = new DetectionEngine.Engine();

        public void Input(Bitmap bmp)
        {
            ProgressPipe.Instance.ProgressMessage("Przygotowywanie obrazu do dalszego przetwarzania...");
            var output = Process(bmp);
            _detectionEngine.Input(output); 
        }

        public List<Mat> Process(Bitmap input)
        {
            var list = new List<Mat>();
            var image = OpenCvSharp.Extensions.BitmapConverter.ToMat(input);
            var cannyDetector = new CannyDetector();
            var thresholder = new Thresholder();
            try
            {
                image = image.CvtColor(ColorConversion.BgrToGray);
            }
            catch (Exception)
            {
                // ignored
            }
            list.Add(cannyDetector.Detect(image));
            list.Add(thresholder.Process(image));
            list.Add(image);

            return list;
        }
    }
}
