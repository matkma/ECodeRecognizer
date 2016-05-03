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
        public void PreparationEngineTest()
        {
            var engine = new Engine();
            var output = engine.Process(new Bitmap(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestData\PreparationTests\test.jpg"));
            bool result;
            using (new Window(GetType().ToString() + " (wciśnij ENTER jeśli obraz jest poprawny)", output[1]))
            {
               result = (Key)Cv2.WaitKey() == Key.Escape;
               if (result)
               {
                   OpenCvSharp.Extensions.BitmapConverter.ToBitmap(output[0]).Save(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestResults\PreparationTests\result0.bmp", ImageFormat.Bmp);
                   OpenCvSharp.Extensions.BitmapConverter.ToBitmap(output[1]).Save(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestResults\PreparationTests\result1.bmp", ImageFormat.Bmp);
                   OpenCvSharp.Extensions.BitmapConverter.ToBitmap(output[2]).Save(@"D:\Projektz\inżynierka\ECodeRecognizer\UnitTests\Test\TestResults\PreparationTests\result2.bmp", ImageFormat.Bmp);
               }
            }
            Assert.IsTrue(result, "Obraz niezgodny z oczekiwaniami.");
        }
    }
}
