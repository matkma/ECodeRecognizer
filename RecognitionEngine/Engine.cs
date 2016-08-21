using System;
using System.Collections.Generic;
using System.IO;
using ControlPipeline;
using NeuralNetworks;
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
            string output = "";
            foreach (var mat in input)
            {
                output += RecognitionController.Instance.CalculateOutput(mat);
            }
            return int.Parse(output.Trim());
        }
    }
}
