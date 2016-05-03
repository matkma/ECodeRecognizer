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
            image = image.PyrDown();
            Mat gray;
            try
            {
                gray = image.CvtColor(ColorConversion.BgrToGray);
            }
            catch (Exception)
            {
                gray = image;
            }
            list.Add(cannyDetector.Detect(gray));
            list.Add(thresholder.Process(gray));
            list.Add(gray);

            return list;
        }
    }
}
