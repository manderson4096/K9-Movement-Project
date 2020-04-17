using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace k9_windows
{
    public partial class Form1 : Form
    {
        bool _streaming;
        Capture _capture;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _capture = new Capture();
            _streaming = false;

        }

        //The event handler chosen for this is application.idle
        //This might have to change later down the road since whenever the image is in the process of being saved, the webcam ceases to stream
        //data to the program
        private void btnStreamOnOff_Click(object sender, EventArgs e)
        {
            if(_streaming == false)
            {
                //code to start streaming
                Application.Idle += streaming;//turns streaming on, event handler for click 
                btnStreamOnOff.Text = @"Stop Streaming"; //updates button text
                _streaming = true; //reset streaming bool
            }
            else
            {
                //code to stop streaming
                Application.Idle -= streaming; //turns streaming off, event handler for click
                btnStreamOnOff.Text = @"Start Streaming"; //updates button text
                _streaming = false;//reset streaming bool
            }
        }

        private void button1_Click(object sender, EventArgs e)//Event handler for capture button
        {

            picOutput.Image = picStream.Image;

        }

        private void SveBtn_Click(object sender, EventArgs e)//Event handler to Save image that was captured
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                picOutput.Image.Save(saveFileDialog.FileName+ ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//saves captured image to any file format you want
                MessageBox.Show(@"Saved!");
            }


        }

        //streaming event handler

        private void streaming(object sender, System.EventArgs e) //defining event handler for clicking streaming button 
        {
            
            var img = _capture.QueryFrame().ToImage<Bgr, byte>();
            var bmp = img.Bitmap;
            picStream.Image = bmp;

        }


    }
}
