using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp.Extensions;
using RecognitionEngine;

namespace UnitTests
{
    [TestClass]
    public class RecognitionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Bitmap bmp = new Bitmap(@"D:\Projektz\inżynierka\Szablon Inputu Sieci\0\022.bmp");
            Mat mat = BitmapConverter.ToMat(bmp);

            var data1 = DataConverter.NetworkInputFromBitmap(bmp);
            var data2 = DataConverter.NetworkInputFromMat(mat);

            for(int i = 0; i < data1.Length; i++)
            {
                Console.WriteLine(data1[i] + "   " + data2[i]);
            }
        }
    }
}
