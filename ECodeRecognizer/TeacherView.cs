using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlPipeline;
using RecognitionEngine;

namespace ECodeRecognizer
{
    public partial class TeacherView : Form
    {
        private List<Bitmap> bitmapList = new List<Bitmap>();
        private int digit;

        public TeacherView()
        {
            InitializeComponent();
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {
            if (passwordBox.Text.Equals("neuralnetwork"))
            {
                controlPanel.Enabled = true;
                controlPanel.Visible = true;
            }
            else
            {
                controlPanel.Enabled = false;
                controlPanel.Visible = false;
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = true,
                AddExtension = true,
                Filter = "Image Files (*.bmp)|*.bmp"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (dialog.FileNames.Length > 0)
                    {
                        bitmapList.Clear();
                        foreach (var name in dialog.FileNames)
                        {
                            bitmapList.Add(new Bitmap(name));
                        }
                        if (BoxContainsDigit())
                        {
                            startButton.Enabled = true;
                        }
                        dataLabel.Text = "Podano " + dialog.FileNames.Length.ToString() + " bitmap.";
                    }
                }
                catch (Exception ex)
                {
                    ErrorPipe.Instance.ErrorMessage(this, "Błąd wczytywania bitmap. Exception: " + ex.Message);
                }
            }
            else
            {
                startButton.Enabled = false;
                dataLabel.Text = "";
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            NeuralNetworkController.Instance.TrainNetwork(bitmapList, digit);
        }

        private bool BoxContainsDigit()
        {
            int result;
            if (digitBox.Text.Length <= 0) return false;
            if (!int.TryParse(digitBox.Text, out result)) return false;
            if (result < 0 || result > 9) return false;
            digit = result;
            return true;
        }

        private void digitBox_TextChanged(object sender, EventArgs e)
        {
            if (BoxContainsDigit() && bitmapList.Count > 0)
            {
                startButton.Enabled = true;
            }
            else
            {
                startButton.Enabled = false;
            }
        }
    }
}
