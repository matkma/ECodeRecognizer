namespace ECodeRecognizer
{
    partial class View
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_AddImage = new System.Windows.Forms.Button();
            this.btn_Process = new System.Windows.Forms.Button();
            this.resultBox = new System.Windows.Forms.ListBox();
            this.btn_teach = new System.Windows.Forms.Button();
            this.progressLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.BackColor = System.Drawing.Color.Khaki;
            this.imageBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.imageBox.Location = new System.Drawing.Point(-2, -1);
            this.imageBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(400, 400);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imageBox.TabIndex = 0;
            this.imageBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.imageBox);
            this.panel1.Location = new System.Drawing.Point(31, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 400);
            this.panel1.TabIndex = 1;
            // 
            // btn_AddImage
            // 
            this.btn_AddImage.BackColor = System.Drawing.Color.Khaki;
            this.btn_AddImage.Font = new System.Drawing.Font("Adobe Heiti Std R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_AddImage.Location = new System.Drawing.Point(479, 30);
            this.btn_AddImage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_AddImage.Name = "btn_AddImage";
            this.btn_AddImage.Size = new System.Drawing.Size(160, 40);
            this.btn_AddImage.TabIndex = 2;
            this.btn_AddImage.Text = "DODAJ OBRAZ\r\n";
            this.btn_AddImage.UseVisualStyleBackColor = false;
            this.btn_AddImage.Click += new System.EventHandler(this.btn_AddImage_Click);
            // 
            // btn_Process
            // 
            this.btn_Process.BackColor = System.Drawing.Color.Khaki;
            this.btn_Process.Enabled = false;
            this.btn_Process.Font = new System.Drawing.Font("Adobe Heiti Std R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_Process.Location = new System.Drawing.Point(479, 90);
            this.btn_Process.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Process.Name = "btn_Process";
            this.btn_Process.Size = new System.Drawing.Size(160, 40);
            this.btn_Process.TabIndex = 3;
            this.btn_Process.Text = "PRZETWARZAJ";
            this.btn_Process.UseVisualStyleBackColor = false;
            this.btn_Process.Click += new System.EventHandler(this.btn_Process_Click);
            // 
            // resultBox
            // 
            this.resultBox.BackColor = System.Drawing.Color.Khaki;
            this.resultBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resultBox.Font = new System.Drawing.Font("Adobe Heiti Std R", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.resultBox.FormattingEnabled = true;
            this.resultBox.HorizontalScrollbar = true;
            this.resultBox.ItemHeight = 16;
            this.resultBox.Location = new System.Drawing.Point(479, 157);
            this.resultBox.Name = "resultBox";
            this.resultBox.Size = new System.Drawing.Size(160, 194);
            this.resultBox.TabIndex = 4;
            // 
            // btn_teach
            // 
            this.btn_teach.BackColor = System.Drawing.Color.Khaki;
            this.btn_teach.Font = new System.Drawing.Font("Adobe Heiti Std R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_teach.Location = new System.Drawing.Point(479, 390);
            this.btn_teach.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_teach.Name = "btn_teach";
            this.btn_teach.Size = new System.Drawing.Size(160, 40);
            this.btn_teach.TabIndex = 5;
            this.btn_teach.Text = "UCZ";
            this.btn_teach.UseVisualStyleBackColor = false;
            this.btn_teach.Click += new System.EventHandler(this.btn_teach_Click);
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Font = new System.Drawing.Font("Adobe Heiti Std R", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.progressLabel.Location = new System.Drawing.Point(27, 445);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(0, 20);
            this.progressLabel.TabIndex = 6;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(684, 491);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.btn_teach);
            this.Controls.Add(this.resultBox);
            this.Controls.Add(this.btn_Process);
            this.Controls.Add(this.btn_AddImage);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "View";
            this.Text = "E-Code Recognizer";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_AddImage;
        private System.Windows.Forms.Button btn_Process;
        private System.Windows.Forms.ListBox resultBox;
        private System.Windows.Forms.Button btn_teach;
        private System.Windows.Forms.Label progressLabel;
    }
}

