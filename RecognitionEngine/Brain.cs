using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.Linq;

namespace RecognitionEngine
{
    public class Brain
    {
        private List<bool[]> knownPatterns;
        private List<bool[]> knownOutputs;
        private int patternLength = 15*30;
        private int outputLength = 16;
        private double dValue;
        private string filePath = @"Data/RecognitionEngine.xml";

        public Brain()
        {
            knownPatterns = new List<bool[]>();
            knownOutputs = new List<bool[]>();
            dValue = 1.0 / (double)patternLength;

            if (File.Exists(filePath))
            {
                LoadFromFile(filePath);
            }
        }

        public bool[] RecognizePattern(bool[] input)
        {
            var values = Compare(input).ToList();

            return FirstDecision(values);
        }

        public void LearnPatterns(List<bool[]> input, bool[] output)
        {
            foreach (var pattern in input)
            {
                knownPatterns.Add(pattern);
                knownOutputs.Add(output);
            }
        }

        public void SaveToFile()
        {
            if (knownPatterns.Count <= 0) return;

            int numberOfPatterns = knownPatterns.Count;

            using (XmlWriter writer = XmlWriter.Create(filePath))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Patterns");

                for (int i = 0; i < numberOfPatterns; i++)
                {
                    writer.WriteStartElement("Pattern");
                    writer.WriteElementString("Input", DataConverter.BoolArrayToString(knownPatterns[i]));
                    writer.WriteElementString("Output", DataConverter.BoolArrayToString(knownOutputs[i]));
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        private void LoadFromFile(string path)
        {
            XDocument xml = XDocument.Load(path);
            XElement patterns = xml.Element("Patterns");
            if (patterns != null)
            {
                foreach (var pattern in patterns.Elements("Pattern"))
                {
                    var input = pattern.Element("Input");
                    var output = pattern.Element("Output");
                    knownPatterns.Add(DataConverter.StringToBoolArray(input.Value));
                    knownOutputs.Add(DataConverter.StringToBoolArray(output.Value));
                }
            }
        }

        private IEnumerable<double> Compare(bool[] input)
        {
            int numberOfPatterns = knownPatterns.Count;

            for (int i = 0; i < numberOfPatterns; i++)
            {
                double value = 0.0;
                bool[] pattern = knownPatterns[i];

                for (int j = 0; j < patternLength; j++)
                {
                    if (input[j] == pattern[j])
                    {
                        value += dValue;
                    }
                }

                yield return value;
            }
        }

        private bool[] FirstDecision(List<double> values)
        {
            int index = values.IndexOf(values.Max());
            return knownOutputs[index];
        }
    }
}
