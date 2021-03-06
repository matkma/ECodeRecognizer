﻿using System;
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
            var eDetector = new EDetector();
            var digits = new List<Mat>();
            var image = new Mat();

            input[1].CopyTo(image);
            var bounds = contourDetector.DetectLetters(input[1]);
            var capitalEs = eDetector.DetectCapitalE(image, bounds);

            foreach (var capitalE in capitalEs)
            {
                digits.AddRange(ExtractCodes(image, capitalE));
            }

            //foreach (var digit in digits)
            //{
            //    using (new Window(GetType().ToString() + " (wciśnij ENTER jeśli obraz jest poprawny)", digit))
            //    {
            //        Cv2.WaitKey();
            //    }
            //}

            return digits;
        }

        private List<Mat> ExtractCodes(Mat image, Rect capitalE)
        {
            var digits = new List<Mat>();
            var bound = new Rect(capitalE.Location, new Size(capitalE.Height / 2, capitalE.Height));
            image = image.Threshold(100, 255, ThresholdType.Binary);
            try
            {
                digits.Add(new Mat(image, new Rect(new Point(bound.Right, bound.Top), bound.Size)).Resize(new Size(15, 30)));
                digits.Add(new Mat(image, new Rect(new Point(bound.Right + bound.Width, bound.Top), bound.Size)).Resize(new Size(15, 30)));
                digits.Add(new Mat(image, new Rect(new Point(bound.Right + 2 * bound.Width, bound.Top), bound.Size)).Resize(new Size(15, 30)));
            }
            catch (Exception)
            {
                ErrorPipe.Instance.ErrorMessage(this, "Bład wykrycia cyfr w E-kodzie w metodzie ExtractCodes. Przekroczono wymiar obrazu.");
            }
            return digits;

            //var bound = new Rect(capitalE.Location, new Size(capitalE.Height / 2, capitalE.Height));
            //Cv2.Rectangle(image, bound, new Scalar(0, 255, 0), 1);
            //Cv2.Rectangle(image, new Rect(new Point(bound.Left, bound.Top), bound.Size), new Scalar(255, 255, 255), 2);
            //Cv2.Rectangle(image, new Rect(new Point(bound.Right, bound.Top), bound.Size), new Scalar(255, 255, 255), 2);
            //Cv2.Rectangle(image, new Rect(new Point(bound.Right + bound.Width, bound.Top), bound.Size), new Scalar(255, 255, 255), 2);
            //Cv2.Rectangle(image, new Rect(new Point(bound.Right + 2 * bound.Width, bound.Top), bound.Size), new Scalar(255, 255, 255), 2);
            //using (new Window(GetType().ToString() + " (wciśnij ENTER jeśli obraz jest poprawny)", image))
            //{
            //    Cv2.WaitKey();
            //}
            //return new List<Mat>();
        }
    }
}
