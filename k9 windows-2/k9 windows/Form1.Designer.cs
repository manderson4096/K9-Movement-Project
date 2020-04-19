namespace k9_windows
{
    partial class Form1
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
            this.picStream = new System.Windows.Forms.PictureBox();
            this.btnStreamOnOff = new System.Windows.Forms.Button();
            this.SveBtn = new System.Windows.Forms.Button();
            this.btnAnalyze = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picStream)).BeginInit();
            this.SuspendLayout();
            // 
            // picStream
            // 
            this.picStream.Location = new System.Drawing.Point(56, 34);
            this.picStream.Name = "picStream";
            this.picStream.Size = new System.Drawing.Size(299, 263);
            this.picStream.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStream.TabIndex = 2;
            this.picStream.TabStop = false;
            // 
            // btnStreamOnOff
            // 
            this.btnStreamOnOff.Location = new System.Drawing.Point(56, 317);
            this.btnStreamOnOff.Name = "btnStreamOnOff";
            this.btnStreamOnOff.Size = new System.Drawing.Size(148, 34);
            this.btnStreamOnOff.TabIndex = 3;
            this.btnStreamOnOff.Text = "Start Stream";
            this.btnStreamOnOff.UseVisualStyleBackColor = true;
            this.btnStreamOnOff.Click += new System.EventHandler(this.btnStreamOnOff_Click);
            // 
            // SveBtn
            // 
            this.SveBtn.Location = new System.Drawing.Point(280, 388);
            this.SveBtn.Name = "SveBtn";
            this.SveBtn.Size = new System.Drawing.Size(75, 23);
            this.SveBtn.TabIndex = 5;
            this.SveBtn.Text = "Save";
            this.SveBtn.UseVisualStyleBackColor = true;
            this.SveBtn.Click += new System.EventHandler(this.SveBtn_Click);
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(56, 383);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(176, 33);
            this.btnAnalyze.TabIndex = 7;
            this.btnAnalyze.Text = "Analyze Image";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 447);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.SveBtn);
            this.Controls.Add(this.btnStreamOnOff);
            this.Controls.Add(this.picStream);
            this.Name = "Form1";
            this.Text = "WebCam Output";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picStream)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picStream;
        private System.Windows.Forms.Button btnStreamOnOff;
        private System.Windows.Forms.Button SveBtn;
        private System.Windows.Forms.Button btnAnalyze;
    }
}

