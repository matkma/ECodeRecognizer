using System.Collections.Generic;
using System.Globalization;
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
            var eDetector = new EDetector();
            var digits = new List<Mat>();
            var bounds = contourDetector.DetectLetters(input[1]);
            var capitalEs = eDetector.DetectCapitalE(input[0], bounds);
            var image = input[2];

            foreach (var capitalE in capitalEs)
            {
                digits.AddRange(ExtractCodes(image, capitalE));
            }
            using (new Window(GetType().ToString() + " (wciśnij ENTER jeśli obraz jest poprawny)", image))
            {
                Cv2.WaitKey();
            }

            return digits;
        }

        private List<Mat> ExtractCodes(Mat image, Rect capitalE)
        {
            var digits = new List<Mat>();
            var bound = new Rect(capitalE.Location, new Size(capitalE.Height / 2, capitalE.Height));
            image = image.Threshold(100, 255, ThresholdType.Binary);
            digits.Add(new Mat(image, new Rect(new Point(bound.Right, bound.Top), bound.Size)));
            digits.Add(new Mat(image, new Rect(new Point(bound.Right + bound.Width, bound.Top), bound.Size)));
            digits.Add(new Mat(image, new Rect(new Point(bound.Right + 2 * bound.Width, bound.Top), bound.Size)));

            //var bound = new Rect(capitalE.Location, new Size(capitalE.Height / 2, capitalE.Height));
            //Cv2.Rectangle(image, bound, new Scalar(0, 255, 0), 1);
            //Cv2.Rectangle(image, new Rect(new Point(bound.Left, bound.Top), bound.Size), new Scalar(0, 255, 0), 1);
            //Cv2.Rectangle(image, new Rect(new Point(bound.Right, bound.Top), bound.Size), new Scalar(0, 255, 0), 1);
            //Cv2.Rectangle(image, new Rect(new Point(bound.Right + bound.Width, bound.Top), bound.Size), new Scalar(0, 255, 0), 1);
            //Cv2.Rectangle(image, new Rect(new Point(bound.Right + 2 * bound.Width, bound.Top), bound.Size), new Scalar(0, 255, 0), 1);
            //return new List<Mat>();

            return digits;
        }
    }
}
