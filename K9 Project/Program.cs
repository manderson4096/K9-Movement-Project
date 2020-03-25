using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace K9_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            // read in input image
            Console.WriteLine("Enter a file to analyze: ");
            string imageInput = Console.ReadLine();
            Image<Bgr, Byte> img = new Image<Bgr, byte>(imageInput);

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

            // print detected circles to output file
            Image<Bgr, Byte> circleImage = img.CopyBlank();
            foreach (CircleF circle in circles)
                circleImage.Draw(circle, new Bgr(Color.Green), 2);
            circleImage.Save("output.bmp");

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
                "Stop (no forward movement)" : "Move forward";

            Console.WriteLine(DetermineHorizontal(circles[0], uimage));
            Console.WriteLine(DetermineVertical(circles[0], uimage));
            Console.WriteLine(DetermineMovement(circles[0], uimage));
        }
    }
}
