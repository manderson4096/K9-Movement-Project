using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
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
        Capture _capture; //defining capture object, will instansiate withing event handler
        Capture _analyzecapture;//defining capture object, will instansiate withing event handler

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _capture = new Capture();
            _analyzecapture = new Capture();
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



        private void SveBtn_Click(object sender, EventArgs e)//Event handler to Save image that was captured
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                picStream.Image.Save(saveFileDialog.FileName+ ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//saves captured image to any file format you want
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

        //event handler for hitting the analyze button
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            var img = _analyzecapture.QueryFrame().ToImage<Bgr, byte>();

            // read in input image
            // Console.WriteLine("Enter a file to analyze: ");
            //string imageInput = Console.ReadLine();
            //Image<Bgr, Byte> img = new Image<Bgr, byte>(imageInput);

            // convert image to grayscale
            UMat uimage = new UMat();
            CvInvoke.CvtColor(img, uimage, ColorConversion.Bgr2Gray);

            // remove noise from image
            UMat pyrDown = new UMat();
            CvInvoke.PyrDown(uimage, pyrDown);
            CvInvoke.PyrUp(pyrDown, uimage);

            // find the circles
            double cannyThreshold = 180.0;
            double circleAccumulatorThreshold = 120;
            CircleF[] circles = CvInvoke.HoughCircles(uimage, HoughType.Gradient, 2.0, 20.0, cannyThreshold, circleAccumulatorThreshold, 5);


            // find edges
            double cannyThresholdLinking = 120.0;
            UMat cannyEdges = new UMat();
            CvInvoke.Canny(uimage, cannyEdges, cannyThreshold, cannyThresholdLinking);

            LineSegment2D[] lines = CvInvoke.HoughLinesP(
               cannyEdges,
               1,               //Distance resolution in pixel-related units
               Math.PI / 45.0,  //Angle resolution measured in radians.
               20,              //threshold
               30,              //min Line width
               10);             //gap between lines


            // find rectangles
            List<RotatedRect> boxList = new List<RotatedRect>();

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(cannyEdges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                int count = contours.Size;
                for (int i = 0; i < count; i++)
                {
                    using (VectorOfPoint contour = contours[i])
                    using (VectorOfPoint approxContour = new VectorOfPoint())
                    {
                        CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                        if (CvInvoke.ContourArea(approxContour, false) > 250) //only consider contours with area greater than 250
                        {
                            if (approxContour.Size == 4) // rectangles have 4 contours (vertices)
                            {
                                bool isRectangle = true;
                                Point[] pts = approxContour.ToArray();
                                LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                                for (int j = 0; j < edges.Length; j++)
                                {
                                    double angle = Math.Abs(
                                       edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));
                                    if (angle < 80 || angle > 100)
                                    {
                                        isRectangle = false;
                                        break;
                                    }
                                }

                                if (isRectangle) boxList.Add(CvInvoke.MinAreaRect(approxContour));
                            }
                        }
                    }
                }
            }

            // print detected rectangles to output file
            Image<Bgr, Byte> rectangleImage = img.CopyBlank();
            foreach (RotatedRect box in boxList)
                rectangleImage.Draw(box, new Bgr(Color.DarkOrange), 2);
            rectangleImage.Save("output-rectangles.bmp");

            // print detected circles to output file
            Image<Bgr, Byte> circleImage = img.CopyBlank();
            foreach (CircleF circle in circles)
                circleImage.Draw(circle, new Bgr(Color.Green), 2);
            circleImage.Save("output-circles.bmp");


            // function to detect if target center X values match
            bool detectTargetX(CircleF circle, RotatedRect rectangle) =>
                (circle.Center.X > (rectangle.Center.X) * 0.99) &&
                (circle.Center.X < (rectangle.Center.X) * 1.01) ? true : false;

            // function to detect if target center Y values match
            bool detectTargetY(CircleF circle, RotatedRect rectangle) =>
                (circle.Center.Y > (rectangle.Center.Y) * 0.99) &&
                (circle.Center.Y < (rectangle.Center.Y) * 1.01) ? true : false;

            // checking for target in image
            bool targetFound = false;
            CircleF targetCircle = new CircleF();
            foreach (CircleF circle in circles)
            {
                foreach (RotatedRect box in boxList)
                {
                    if (detectTargetX(circle, box) == true) // check if X values match
                    {
                        if (detectTargetY(circle, box) == true) // check if Y values match
                        {
                            //Console.WriteLine("Target is found at " + circle.Center);
                            MessageBox.Show("Target is found at " + circle.Center);
                            targetCircle = circle;
                            targetFound = true;
                            break;
                        }
                        else
                            continue;

                    }

                    else if (targetFound == true) // if target is found, stop the loop
                        break;

                    else
                        continue;
                }
            }


            // find image zones used for movement determinations
            double FindLeftBound(UMat image) => image.Size.Width * 0.45;
            double FindRightBound(UMat image) => image.Size.Width * 0.55;
            double FindUpperBound(UMat image) => image.Size.Height * 0.40;
            double FindLowerBound(UMat image) => image.Size.Height * 0.60;

            // determine if K9 needs to move left or right
            string DetermineHorizontal(CircleF target, UMat image) =>
                target.Center.X < FindLeftBound(image) ?
                "Move right to center the target" : target.Center.X > FindRightBound(image) ?
                "Move left to center the target" : "Target is horizontally centered";

            // determine if K9 needs to tilt head up or down
            string DetermineVertical(CircleF target, UMat image) =>
                target.Center.Y > FindLowerBound(image) ?
                "Tilt head up to center the target" : target.Center.Y < FindUpperBound(image) ?
                "Tilt head down to center the target" : "Target is vertically centered";

            // find image area limit that the target should fill
            double FindAreaLimit(UMat image) => image.Size.Height * image.Size.Width * 0.10;

            // determine if K9 needs to move forward
            string DetermineMovement(CircleF target, UMat image) =>
                target.Area > FindAreaLimit(image) ?
                "Stop (no forward movement" : "Move forward";

            if (targetFound == true)
            {
                
                MessageBox.Show(DetermineHorizontal(targetCircle, uimage) +". \n\n"+ DetermineVertical(targetCircle, uimage)+ ". \n\n" + DetermineMovement(targetCircle, uimage) + ".");
                
            }
            else
               //Console.WriteLine("Target was not found.");
                MessageBox.Show("Target was not found.");
        }
        
    }
}
