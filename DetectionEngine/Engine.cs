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
            var contourDetector = new LettersDetector();
            var bounds = contourDetector.DetectLetters(input[1]);
            var image = input[2];
            foreach (var bound in bounds)
            {
                Cv2.Rectangle(image, bound, new Scalar(0, 255, 0), 1);
            }
            return input;
        }
    }
}
