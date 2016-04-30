using System.Collections.Generic;
using ControlPipeline;
using OpenCvSharp.CPlusPlus;

namespace DetectionEngine
{
    public class Engine
    {
        private RecognitionEngine.Engine recognitionEngine = new RecognitionEngine.Engine();

        public void Input(Mat input)
        {
            ProgressPipe.Instance.ProgressMessage("Wykrywanie E-kodów...");
            Process(input);
        }

        private void Process(Mat input)
        {
            input.Resize(new Size(100, 100));
            var digits = new List<Mat> {input, input, input};
            recognitionEngine.Input(digits);
        }
    }
}
