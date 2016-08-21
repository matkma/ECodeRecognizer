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
        private Brain brain;

        public void Train(List<bool[]> input, bool[] output, Brain student)
        {
            brain = student;
            BackgroundWorker teacher = new BackgroundWorker();
            teacher.DoWork += delegate
            {
                brain.LearnPatterns(input, output);
            };

            teacher.WorkerReportsProgress = true;
            teacher.RunWorkerCompleted += TrainingCompleted;
            teacher.RunWorkerAsync();
        }

        private void TrainingCompleted(object sender, EventArgs e)
        {
            MessageBox.Show("Uczenie zakończone", "Uwaga!", MessageBoxButtons.OK);
            brain.SaveToFile();
        }
    }
}
