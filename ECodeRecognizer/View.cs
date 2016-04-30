using System;
using System.Drawing;
using System.Windows.Forms;
using ControlPipeline;

namespace ECodeRecognizer
{
    public partial class View : Form
    {
        private Bitmap image;
        private PreparationEngine.Engine engine = new PreparationEngine.Engine();

        public View()
        {
            InitializeComponent();
            ProgressPipe.Instance.ProgressEvent += this.HandleProgressEvent;
            DataPipe.Instance.DataEvent += this.HandleDataEvent;
            ErrorPipe.Instance.ErrorEvent += this.HandleErrorEvent;
        }

        private void btn_AddImage_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = @"JPG Files (.jpg)|*.jpg|BMP Files (.bmp*)|*.bmp|PNG Files (.png)|*.png",
                FilterIndex = 1,
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                image = new Bitmap(openFileDialog.FileName);
                imageBox.Image = image;
            }
            catch (Exception)
            {
                return;
            }

            btn_Process.Enabled = true;
        }

        private void btn_Process_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                engine.Input(image);
            }
        }

        private void HandleProgressEvent(object sender, ProgressEventArgs args)
        {
            progressLabel.Text = args.GetInfo().eventInfo;
        }

        private void HandleDataEvent(object sender, DataEventArgs args)
        {
            ProgressPipe.Instance.ProgressMessage("Sukces.");
            resultBox.Items.Clear();
            foreach (var ecode in args.GetInfo())
            {
                var item = ecode.code + ": " + ecode.name;
                if (ecode.description != "")
                {
                    item += " (" + ecode.description + ")";
                }
                resultBox.Items.Add(item);
            }
        }

        private void HandleErrorEvent(object sender, ErrorEventArgs args)
        {
            progressLabel.Text = "Przetwarzanie nieudane.";
            MessageBox.Show(args.GetInfo().eventInfo + 
                            "\nData zalogowania błędu: " + args.GetInfo().beginDateTime, 
                            "ERROR");
        }
    }
}
