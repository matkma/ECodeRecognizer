using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Input;
using DetectionEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp.CPlusPlus;

namespace UnitTests
{
    [TestClass]
    public class DetectionTests
    {
        [TestMethod]
        public void ContourDetectorTest()
        {
            var detector = new LettersDetector();
            var bounds = detector.DetectLetters(OpenCvSharp.Extensions.BitmapConverter.ToMat(new Bitmap(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestData\DetectionTests\ContourDetectorTest\test.bmp")));
            var image = OpenCvSharp.Extensions.BitmapConverter.ToMat(new Bitmap(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestData\DetectionTests\ContourDetectorTest\test1.bmp"));
            foreach (var bound in bounds)
            {
                Cv2.Rectangle(image, bound, new Scalar(0, 255, 0));
            }
            bool result;
            using (new Window(GetType().ToString() + " (wciśnij ENTER jeśli obraz jest poprawny)", image))
            {
                result = (Key)Cv2.WaitKey() == Key.Escape;
                if (result)
                {
                    OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image).Save(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestResults\DetectionTests\ContourDetectorTest\result0.bmp", ImageFormat.Bmp);
                }
            }
            Assert.IsTrue(result, "Obraz niezgodny z oczekiwaniami.");
        }
    }
}
