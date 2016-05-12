using System;
using System.CodeDom;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp.CPlusPlus;
using PreparationEngine;

namespace UnitTests
{
    [TestClass]
    public class PreparationTests
    {
        [TestMethod]
        public void PreparationEngineTest1()
        {
            Test(1);
        }

        [TestMethod]
        public void PreparationEngineTest2()
        {
            Test(2);
        }

        private void Test(int test)
        {
            var engine = new Engine();
            var output = engine.Process(new Bitmap(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestData\PreparationTests\" + test + @"\test.jpg"));
            bool result;
            using (new Window(GetType().ToString() + " (wciśnij ENTER jeśli obraz jest poprawny)", output[1]))
            {
                result = (Key)Cv2.WaitKey() == Key.Escape;
                if (result)
                {
                    OpenCvSharp.Extensions.BitmapConverter.ToBitmap(output[0]).Save(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestResults\PreparationTests\" + test + @"\result0.bmp", ImageFormat.Bmp);
                    OpenCvSharp.Extensions.BitmapConverter.ToBitmap(output[1]).Save(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestResults\PreparationTests\" + test + @"\result1.bmp", ImageFormat.Bmp);
                    OpenCvSharp.Extensions.BitmapConverter.ToBitmap(output[2]).Save(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestResults\PreparationTests\" + test + @"\result2.bmp", ImageFormat.Bmp);
                }
            }
            Assert.IsTrue(result, "Obraz niezgodny z oczekiwaniami.");
        }
    }
}
