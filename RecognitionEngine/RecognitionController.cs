using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Neuro;
using NeuralNetworks;
using OpenCvSharp.CPlusPlus;

namespace RecognitionEngine
{
    public class RecognitionController
    {
        public Brain Brain { get; set; }

        private static RecognitionController _instance;
        public static RecognitionController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RecognitionController();
                }
                return _instance;
            }
        }

        public RecognitionController()
        {
            Brain = new Brain();
        }

        public string CalculateOutput(Mat data)
        {   
            var output = Brain.RecognizePattern(DataConverter.BrainInputFromMat(data));
            return DataConverter.BrainOutputToDigit(output);
        }

        public void TrainEngine(List<Bitmap> data, int digit)
        {
            var teacher = new Teacher();
            teacher.Train
            (
                data.Select(DataConverter.BrainInputFromBitmap).ToList(),
                DataConverter.DigitToBrainOutput(digit),
                Brain
            );
        }
    }
}
