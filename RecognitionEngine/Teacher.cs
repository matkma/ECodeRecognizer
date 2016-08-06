using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuralNetworks;

namespace RecognitionEngine
{
    internal class Teacher
    {
        private BackPropogationNetwork network;

        public void TrainNetwork(List<DataSet> data, BackPropogationNetwork inputNetwork)
        {
            network = inputNetwork;
            BackgroundWorker teacher = new BackgroundWorker();
            teacher.DoWork += delegate
            {
                network.BatchBackPropogate(data.ToArray(), 40, 0.5, 0.2, teacher);
            };

            teacher.WorkerReportsProgress = true;
            teacher.RunWorkerCompleted += TrainingCompleted;
            teacher.RunWorkerAsync();
        }

        private void TrainingCompleted(object sender, EventArgs e)
        {
            MessageBox.Show("Uczenie zakończone", "Uwaga!", MessageBoxButtons.OK);
            SaveNetwork();
        }

        private void SaveNetwork()
        {
            NeuralNetwork.SaveNetworkToFile(network.GetNetworkData(), @"Data/Network.xml");
        }
    }
}
