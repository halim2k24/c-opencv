using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

//using OpenCvSharp.Extensions;

namespace opncvpro
{
    public partial class Form1 : Form
    {
        private Image bitmap;

        public object BitmapConverter { get; private set; }
        public Mat SelectionRangeConverter { get; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Mat img = Cv2.ImRead();

            //string imagePath = openFileDialog.FileName;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                Mat colorImage = Cv2.ImRead(imagePath);
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));

                Cv2.ImShow("Resized Grayscale Image", resizedImage);
                // Cv2.ImWrite(@"C:\Users\ykoma\Desktop\ハリム\c#\opncvpro\img\save1.png", resizedImage);

            }

        }


        private void button1_Click_1(object sender, EventArgs e)
        {

            Mat mat = new Mat(230, 240, MatType.CV_8UC3, new Scalar(0, 0, 255));
            Mat mat1 = new Mat(new OpenCvSharp.Size(130, 140), MatType.CV_8UC3, new Scalar(0, 255, 0));
            Cv2.ImShow("m", mat);
            Cv2.ImShow("m1", mat1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image in color
                Mat colorImage = Cv2.ImRead(imagePath);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }


                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));

                Cv2.ImShow("Resized  Image", resizedImage);
                Cv2.ImWrite(@"C:\Users\ykoma\Desktop\ハリム\c#\opncvpro\img\save1.png", resizedImage);


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image in color
                Mat colorImage = Cv2.ImRead(imagePath);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }


                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));

                Cv2.ImShow("Resized  Image", resizedImage);

                Mat[] channels;
                Cv2.Split(resizedImage, out channels);
                Cv2.ImShow("Blue",channels[0]);
                Cv2.ImShow("Green",channels[1]);
                Cv2.ImShow("Red",channels[2]);

                // rison of intrest value change to show some parts of img.
                Mat rioImg = new Mat(resizedImage,new Rect(50,50,250,250));
                Cv2.ImShow("rio", rioImg);


                // Cv2.ImWrite(@"C:\Users\ykoma\Desktop\ハリム\c#\opncvpro\img\save1.png", resizedImage);


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image in color
                Mat colorImage = Cv2.ImRead(imagePath);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }

                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));

                Cv2.ImShow("Resized  Image", resizedImage);


                // rison of intrest value change to show some parts of img.
                Mat rioImg = new Mat(resizedImage, new Rect(50, 50, 250, 250));
                Cv2.ImShow("rio", rioImg);
                // Cv2.ImWrite(@"C:\Users\ykoma\Desktop\ハリム\c#\opncvpro\img\save1.png", resizedImage);


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image in color
                Mat colorImage = Cv2.ImRead(imagePath);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }

                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));

                //Cv2.ImShow("Resized  Image", resizedImage);


                // rison of intrest value change to show some parts of img.
                Mat canvas = new Mat(resizedImage, new Rect(50, 50, 250, 250));

                Cv2.ImShow("canvus", canvas);

                var red = new Scalar(0, 0, 255);
                var blue = new Scalar(255, 0, 0);
                var green = new Scalar(0, 255, 0);
                var white = new Scalar(255, 255, 255);
                Cv2.Line(canvas, new OpenCvSharp.Point(20, 20), new OpenCvSharp.Point(200, 200), blue, 2);
                Cv2.Circle(canvas, new OpenCvSharp.Point(60, 60), 30, red, 3);
                Cv2.Rectangle(canvas, new Rect(new OpenCvSharp.Point(60, 60), new OpenCvSharp.Size(50, 50)), green, 2);
                Cv2.ImShow("canvus", canvas);

            }


        }

        private void button6_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image in color
                Mat colorImage = Cv2.ImRead(imagePath);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }
                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));
                // Cv2.ImShow("canvus", colorImage);

                float[] data = { 1,0,50,0,1,50};
                Mat M = new Mat(2,3, MatType.CV_32FC1,data);
                Mat dest = new Mat();
                Cv2.WarpAffine(resizedImage, dest,M, new OpenCvSharp.Size(colorImage.Width+60,colorImage.Height+60));
                Cv2.ImShow("test",dest);


            }

        }

        private void button7_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image in color
                Mat colorImage = Cv2.ImRead(imagePath);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }
                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                //Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(0, 0),0.5,0.5);
                Cv2.ImShow("resizedImage 0.5", resizedImage);


                var center = new Point2f(resizedImage.Width/2, resizedImage.Height/2);
                double angle = -45.0;
                Mat M = Cv2.GetRotationMatrix2D(center,angle,1.0);
                Mat dest = new Mat();
                Cv2.WarpAffine(resizedImage, dest, M, new OpenCvSharp.Size(colorImage.Width + 60, colorImage.Height + 60));

                Cv2.ImShow("test", dest);


            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image in color
                Mat colorImage = Cv2.ImRead(imagePath);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }
                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));
                //Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(0, 0), 0.5, 0.5);
                Cv2.ImShow("resizedImage 0.5", resizedImage);

                Mat dis1 = new Mat();
                Mat dis2 = new Mat();
                Mat dis3 = new Mat();
                Cv2.Flip(resizedImage,dis1,FlipMode.X);
                Cv2.Flip(resizedImage,dis2,FlipMode.Y);
                Cv2.Flip(resizedImage,dis3,FlipMode.XY);
                Cv2.ImShow("Dis1 ", dis1);
                Cv2.ImShow("Dis2 ", dis2);
                Cv2.ImShow("Dis3 ", dis3);


            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image in color
                Mat colorImage = Cv2.ImRead(imagePath);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }
                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));
                //Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(0, 0), 0.5, 0.5);
                //Cv2.ImShow("resizedImage ", resizedImage);

                Mat image1 = Mat.Zeros(new OpenCvSharp.Size(400, 400), MatType.CV_8UC1);
                //Mat image1 = resizedImage;
                Mat image2 = Mat.Zeros(new OpenCvSharp.Size(400,400),MatType.CV_8UC1);


                Cv2.Rectangle(image1, new Rect(new OpenCvSharp.Point(0, 0), new OpenCvSharp.Size(image1.Cols / 2, image1.Rows)), new Scalar(255, 255, 255), -1);
                Cv2.ImShow("image1", image1);


                Cv2.Rectangle(image2, new Rect(new OpenCvSharp.Point(150, 100), new OpenCvSharp.Size(200, 50)), new Scalar(255, 255, 255), -1);
                Cv2.ImShow("image2", image2);


                //// and 
                //Mat andOp = new Mat();
                //Cv2.BitwiseAnd(image1, image2, andOp);
                //Cv2.ImShow("andOp", andOp);


                //// or 
                //Mat orOp = new Mat();
                //Cv2.BitwiseOr(image1, image2, orOp);
                //Cv2.ImShow("OrOp", orOp);

                //// xor 
                //Mat xorOp = new Mat();
                //Cv2.BitwiseXor(image1, image2, xorOp);
                //Cv2.ImShow("xorOp", xorOp);

                // Not 
                Mat notOp = new Mat();
                Cv2.BitwiseNot(image1, image2, notOp);
                Cv2.ImShow("notOp", notOp);



            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // normaly thresholding use for grayscale image

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image in color
                Mat colorImage = Cv2.ImRead(imagePath);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }
                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));
                //Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(0, 0), 0.5, 0.5);
                Cv2.ImShow("resizedImage", resizedImage);





                //Mat threshold = new Mat(new OpenCvSharp.Size(resizedImage.Width, resizedImage.Height),MatType.CV_8UC3,new OpenCvSharp.Scalar(0));
                //Cv2.Threshold(resizedImage, threshold,15,255,ThresholdTypes.Binary);
                //Cv2.ImShow("threshold Img", threshold);

                Mat threshold = new Mat();
                Mat grayscaled = new Mat();
                Cv2.CvtColor(resizedImage, grayscaled, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(grayscaled, threshold, 15, 255, ThresholdTypes.Binary);

                Mat adaptive = new Mat();
                Cv2.AdaptiveThreshold(grayscaled, adaptive,255,AdaptiveThresholdTypes.GaussianC,ThresholdTypes.Binary,115,1);
                Cv2.ImShow("adaptive", adaptive);





            }
        }

        private void button11_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image in color
                Mat colorImage = Cv2.ImRead(imagePath);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }
                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));
                //Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(0, 0), 0.5, 0.5);
                Cv2.ImShow("resizedImage", resizedImage);

                Mat grayImage = new Mat();
                Cv2.CvtColor(resizedImage, grayImage, ColorConversionCodes.BGR2GRAY);


                Mat otsu = new Mat();
                Cv2.Threshold(grayImage, otsu,0,255,ThresholdTypes.Otsu | ThresholdTypes.Binary);
                Cv2.ImShow("otsu", otsu);
                Cv2.ImWrite("img/otsu.png", otsu);

                //Mat binaryImage = new Mat();
                //Cv2.Threshold(grayImage, binaryImage, 0, 255, ThresholdTypes.Otsu);

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image and make it gray
                Mat colorImage = Cv2.ImRead(imagePath,ImreadModes.Grayscale);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }
                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));
                //Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(0, 0), 0.5, 0.5);
                Cv2.ImShow("resizedImage", resizedImage);

                Mat histogram = ComputeHistogtam(resizedImage);

                PlotHistogram(histogram);

            }
        }

        //

         static Mat ComputeHistogtam(Mat img)
        {
            Mat histogram = new Mat();

            int[] channel = new int[] { 0};
            int[] histSize = new int[] { 256};

            Rangef[] ranges = {new Rangef(0,256)};

            Cv2.CalcHist(new Mat[] { img},channel,null,histogram,1,histSize,ranges);


            return histogram;
        }

        static void PlotHistogram(Mat histogram)
        {
            int plotWidth = 1024, plotHeight = 400;
            int binWidth = (plotWidth / histogram.Rows);
            Mat canvas = new Mat(plotHeight, plotWidth, MatType.CV_8UC3, new Scalar(0, 0, 0));
            Cv2.Normalize(histogram, histogram, 0, plotHeight, NormTypes.MinMax);
            for (int rows = 1; rows < histogram.Rows; ++rows)
            {
                Cv2.Line(canvas,
                new OpenCvSharp.Point((binWidth * (rows - 1)), (plotHeight - (float)(histogram.At<float>(rows - 1, 0)))),
                new OpenCvSharp.Point(binWidth * rows, (plotHeight - (float)(histogram.At<float>(rows, 0)))),
                new Scalar(125, 125, 125), 2);

            }
            Cv2.ImShow("Histogram", canvas);
        }

        private void button13_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image and make it gray
                Mat colorImage = Cv2.ImRead(imagePath, ImreadModes.Grayscale);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }
                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));
                //Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(0, 0), 0.5, 0.5);
                Cv2.ImShow("resizedImage", resizedImage);



                var kernel3x3 = Mat.Ones(new OpenCvSharp.Size(3, 3), MatType.CV_32F) / 9;
                var kernel5x5 = Mat.Ones(new OpenCvSharp.Size(5, 5), MatType.CV_32F) / 25;
                Mat result3x3 = new Mat();
                Mat result5x5 = new Mat();
                Mat container = new Mat(resizedImage.Height, 3 * (resizedImage.Width) + 20 * 2, MatType.CV_8UC3);
                Mat container1 = new Mat(resizedImage.Height, 3 * (resizedImage.Width) + 20 * 2, MatType.CV_8UC3);


                //Cv2.Filter2D(image, result3x3, -1, kernel3x3);
                //Cv2.Filter2D(image, result5x5, -1, kernel5x5);
                //container[new Rect(new Point(0, 0), new Size(image.Width, image.Height))] = image;
                //container[new Rect(new Point(image.Width + 20, 0), new Size(image.Width, image.Height))] = result3x3;
                //container[new Rect(new Point(2 * image.Width + 40, 0), new Size(image.Width, image.Height))] = result5x5;

                //Cv2.ImShow("Side by Side", container);


                Mat result5x5Gaus = new Mat();
                Mat result5x5Blur = new Mat();
                Cv2.Blur(resizedImage, result5x5Blur, new OpenCvSharp.Size(5, 5)); // size of the filter
                container1[new Rect(new OpenCvSharp.Point(0, 0), new OpenCvSharp.Size(resizedImage.Width, resizedImage.Height))] = resizedImage;
                container1[new Rect(new OpenCvSharp.Point(resizedImage.Width + 20, 0), new OpenCvSharp.Size(resizedImage.Width, resizedImage.Height))] = result5x5Blur;
                

                Cv2.GaussianBlur(resizedImage, result5x5Gaus, new OpenCvSharp.Size(5, 5), 1.5, 1.5); // last parameter controls the shape of the Gaussian
                container1[new Rect(new OpenCvSharp.Point(2 * resizedImage.Width + 40, 0), new OpenCvSharp.Size(resizedImage.Width, resizedImage.Height))] = result5x5Gaus;
                Cv2.ImShow("castle original and blurred and gaussian", container1);

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Load the image and make it gray
                Mat colorImage = Cv2.ImRead(imagePath, ImreadModes.Color);
                if (colorImage.Empty())
                {
                    MessageBox.Show("Failed to load the image.");
                    return;
                }
                // Define the new dimensions
                int newWidth = 400; // New width
                int newHeight = 300; // New height

                // Resize the  image
                Mat resizedImage = new Mat();
                Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(newWidth, newHeight));
                //Cv2.Resize(colorImage, resizedImage, new OpenCvSharp.Size(0, 0), 0.5, 0.5);
                // Cv2.ImShow("resizedImage", resizedImage);

                Cv2.CvtColor(resizedImage, resizedImage, ColorConversionCodes.BGR2GRAY);
                Mat edgesL2 = new Mat();
                Mat edgesL1 = new Mat();

                Cv2.Canny(resizedImage, edgesL2, 125, 350, 3, true);
                Cv2.Canny(resizedImage, edgesL1, 125, 350, 3, false);

                Cv2.ImShow("edgesL2", edgesL2);
                Cv2.ImShow("edgesL1", edgesL1);



            }
        }

   
        //












    }
}
