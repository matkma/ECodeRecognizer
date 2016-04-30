using System;
using System.Collections.Generic;
using ControlPipeline;
using OpenCvSharp.CPlusPlus;

namespace RecognitionEngine
{
    public class Engine
    {
        private DatabaseLayer.DatabaseConnection databaseConnection = new DatabaseLayer.DatabaseConnection();

        public void Input(List<Mat> input)
        {
            ProgressPipe.Instance.ProgressMessage("Rozpoznawanie cyfr...");
            Process(input);
        }

        private void Process(List<Mat> input)
        {
            var output = "";
            Random rand = new Random();

            foreach (var digit in input)
            {
                int x = rand.Next(0, 9);
                output += x.ToString();
            }

            databaseConnection.Input(int.Parse(output.Trim()));
        }
    }
}
