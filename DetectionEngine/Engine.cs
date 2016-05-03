using System;
using System.Collections.Generic;
using ControlPipeline;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

namespace DetectionEngine
{
    public class Engine
    {
        private readonly RecognitionEngine.Engine _recognitionEngine = new RecognitionEngine.Engine();

        public void Input(List<Mat> input)
        {
            ProgressPipe.Instance.ProgressMessage("Wykrywanie E-kodów...");
            var output = Process(input);
            _recognitionEngine.Input(output);
        }

        public List<Mat> Process(List<Mat> input)
        {
            var contourDetector = new ContourDetector();
            var bounds = contourDetector.Detect(input[1]);
            var image = input[2];
            foreach (var bound in bounds)
            {
                Cv2.Rectangle(image, bound, new Scalar(0, 255, 0), 3);
            }
            using (new Window(bounds.Count.ToString(), image))
            {
               Cv2.WaitKey();
            }
            return input;
        }
    }
}
