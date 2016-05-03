using System;
using System.Collections.Generic;
using ControlPipeline;
using OpenCvSharp.CPlusPlus;

namespace RecognitionEngine
{
    public class Engine
    {
        private readonly DatabaseLayer.DatabaseConnection _databaseConnection = new DatabaseLayer.DatabaseConnection();

        public void Input(List<Mat> input)
        {
            ProgressPipe.Instance.ProgressMessage("Rozpoznawanie cyfr...");
            var output = Process(input);
            _databaseConnection.Input(output);
        }

        private int Process(List<Mat> input)
        {
            var output = "";
            Random rand = new Random();

            foreach (var digit in input)
            {
                int x = rand.Next(0, 9);
                output += x.ToString();
            }

            return int.Parse(output.Trim());
        }
    }
}
