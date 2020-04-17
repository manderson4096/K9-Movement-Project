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
            this.picOutput = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.picStream = new System.Windows.Forms.PictureBox();
            this.btnStreamOnOff = new System.Windows.Forms.Button();
            this.SveBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStream)).BeginInit();
            this.SuspendLayout();
            // 
            // picOutput
            // 
            this.picOutput.Location = new System.Drawing.Point(582, 34);
            this.picOutput.Name = "picOutput";
            this.picOutput.Size = new System.Drawing.Size(313, 252);
            this.picOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOutput.TabIndex = 0;
            this.picOutput.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(161, 382);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "Capture";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.btnStreamOnOff.Location = new System.Drawing.Point(126, 326);
            this.btnStreamOnOff.Name = "btnStreamOnOff";
            this.btnStreamOnOff.Size = new System.Drawing.Size(148, 34);
            this.btnStreamOnOff.TabIndex = 3;
            this.btnStreamOnOff.Text = "Start Stream";
            this.btnStreamOnOff.UseVisualStyleBackColor = true;
            this.btnStreamOnOff.Click += new System.EventHandler(this.btnStreamOnOff_Click);
            // 
            // SveBtn
            // 
            this.SveBtn.Location = new System.Drawing.Point(719, 316);
            this.SveBtn.Name = "SveBtn";
            this.SveBtn.Size = new System.Drawing.Size(75, 23);
            this.SveBtn.TabIndex = 5;
            this.SveBtn.Text = "Save";
            this.SveBtn.UseVisualStyleBackColor = true;
            this.SveBtn.Click += new System.EventHandler(this.SveBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 466);
            this.Controls.Add(this.SveBtn);
            this.Controls.Add(this.btnStreamOnOff);
            this.Controls.Add(this.picStream);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.picOutput);
            this.Name = "Form1";
            this.Text = "WebCam Output";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStream)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picOutput;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox picStream;
        private System.Windows.Forms.Button btnStreamOnOff;
        private System.Windows.Forms.Button SveBtn;
    }
}

