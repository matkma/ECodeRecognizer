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
using NeuralNetworks;
using OpenCvSharp.CPlusPlus;

namespace RecognitionEngine
{
    public class NeuralNetworkController
    {
        public BackPropogationNetwork Network { get; set; }

        private static NeuralNetworkController _instance;
        public static NeuralNetworkController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NeuralNetworkController();
                }
                return _instance;
            }
        }

        public NeuralNetworkController()
        {
            Network = LoadNetwork();
        }

        public string CalculateOutput(Mat data)
        {   
            Network.ApplyInput(DataConverter.NetworkInputFromMat(data));
            Network.CalculateOutput();
            return DataConverter.NetworkOutputToDigit(Network.ReadOutput());
        }

        public void TrainNetwork(List<Bitmap> data, int digit)
        {
            var input = data.Select(bitmap => new DataSet
            {
                Outputs = DataConverter.DigitToNetworkOutput(digit),
                Inputs = DataConverter.NetworkInputFromBitmap(bitmap)
            }).ToList();
            var teacher = new Teacher();
            teacher.TrainNetwork(input, Network);
        }

        private BackPropogationNetwork LoadNetwork()
        {
            if (File.Exists(@"Data/Network.xml"))
            {
                var networkData = NeuralNetwork.ReadNetworkFromFile(@"Data/Network.xml");
                return new BackPropogationNetwork(networkData);
            }
            return new BackPropogationNetwork(36, 16, 24);
        }
    }
}
