using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Input;
using DetectionEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp.CPlusPlus;
using Point = OpenCvSharp.CPlusPlus.Point;
using Size = OpenCvSharp.CPlusPlus.Size;

namespace UnitTests
{
    [TestClass]
    public class DetectionTests
    {
        [TestMethod]
        public void DetectionEngineTest1()
        {
            Test(1);
        }

        [TestMethod]
        public void DetectionEngineTest2()
        {
            Test(2);
        }

        private void Test(int test)
        {
            var contourDetector = new LettersDetector();
            var eDetector = new EDetector();
            var letters = contourDetector.DetectLetters(OpenCvSharp.Extensions.BitmapConverter.ToMat(new Bitmap(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestData\DetectionTests\" + test + @"\test.bmp")));
            var capitalEs = eDetector.DetectCapitalE(OpenCvSharp.Extensions.BitmapConverter.ToMat(new Bitmap(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestData\DetectionTests\" + test + @"\test2.bmp")), letters);
            var image = OpenCvSharp.Extensions.BitmapConverter.ToMat(new Bitmap(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestData\DetectionTests\" + test + @"\test1.bmp"));
            foreach (var capitalE in capitalEs)
            {
                var bound = new Rect(capitalE.Location, new Size(capitalE.Height / 2, capitalE.Height));
                Cv2.Rectangle(image, bound, new Scalar(0, 255, 0), 1);
                Cv2.Rectangle(image, new Rect(new Point(bound.Left, bound.Top), bound.Size), new Scalar(0, 255, 0), 1);
                Cv2.Rectangle(image, new Rect(new Point(bound.Right, bound.Top), bound.Size), new Scalar(0, 255, 0), 1);
                Cv2.Rectangle(image, new Rect(new Point(bound.Right + bound.Width, bound.Top), bound.Size), new Scalar(0, 255, 0), 1);
                Cv2.Rectangle(image, new Rect(new Point(bound.Right + 2 * bound.Width, bound.Top), bound.Size), new Scalar(0, 255, 0), 1);
            }
            bool result;
            using (new Window(GetType().ToString() + " (wciśnij ENTER jeśli obraz jest poprawny)", image))
            {
                result = (Key)Cv2.WaitKey() == Key.Escape;
                if (result)
                {
                    OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image).Save(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestResults\DetectionTests\" + test + @"\result0.bmp", ImageFormat.Bmp);
                }
            }
            Assert.IsTrue(result, "Obraz niezgodny z oczekiwaniami.");
        }
    }
}
