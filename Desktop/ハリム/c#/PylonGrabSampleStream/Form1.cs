using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using AForge.Imaging.Filters;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;
using Basler.Pylon;   //  Use for Pylon
using PylonC.NET;                       //Baslerのライブラリプログラムを使うため
using System.IO;
using System.Reflection.Emit;

  //  Use for Pylon


//  ピクセルフォーマットの指定
enum MyPixelFormat
{
    YUV422PACED,    // YUV422 Packed
    YCbCr422,       // YCbCr422
    MONO8,          // モノクロ 8bit
    BG8,            // BG8
};

namespace PylonGrabSampleStream
{
    public partial class Form1 : Form
    {
        // ピクセルフォーマットの指定
        //private MyPixelFormat usePixeFormat = MyPixelFormat.YUV422PACED;
        private MyPixelFormat usePixeFormat = MyPixelFormat.YCbCr422;

        //  画像の幅の指定（0の場合は変更しない）
        private long imageWidth = 0;

        //  画像の高さの指定（0の場合は変更しない）
        private long imageHeight = 0;

        //  露光時間の指定（0の場合は変更しない）
        private double dExposureTime = 10000.0;

        //  パケットサイズの指定（0の場合は変更しない）
        private long packetSize = 8000;

        //  バッファ数(多いほど画像取込に有利だがメモリを食う)
        private long MaxNumBuffer = 10;

        //Cria variavel para conectar com azure custom vision
        public bool avalia = false;

        public int sampleNumber = 0;

        //  カメラクラス
        private Camera camera0 = null;

        //Number of snaps took
        public bool _snaps = false;
        public bool _motor = false;

        public int _snapscount = 0;
        public List<SnapPoints> snapPoints = new List<SnapPoints>();
        public List<Point> centers = new List<Point>();

        private Mat _hsvImage = new Mat();
        private Mat _selectedframe = new Mat();
        private int _selectedconnection = 0;
        private double _selectedangle = 0;


        //  イメージコンバータ
        private PixelDataConverter converter = new PixelDataConverter();

        //  ビットマップ
        public Bitmap bitmap = null;
        public Bitmap bitmap2 = null;



        //  画像取込数
        private long counter;

        //  連続画像取込スレッド
        private Thread GrabThread = null;


        //  連続画像取込停止フラグ
        private AutoResetEvent GrabStopFlag;

        private int cameramode = 0;
        private int pixelformat = 0;

        //　

        //　デバイス数(利用可能なデバイスの数)
        int numDevices;

        //　シリアルナンバー一覧
        string serial0 = (23122998.ToString());//a2A2590-60ucBAS



        public Form1()
        {
            InitializeComponent();
        }
        public class SnapPoints
        {
            public Point StartPoint { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
        }
        //  ビットマップ（System.Drawing.Bitmap）を生成します。
        private void CreateBitmap(out Bitmap bitmap, IGrabResult grabResult)
        {
            bitmap = null;
            try
            {

                //  各ピクセルフォーマットに対応したビットマップの生成を行います。
                if (grabResult.PixelTypeValue == PixelType.Mono8)    //  8ビットモノクロ
                {
                    bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format8bppIndexed);

                    //  パレットの生成を行います。
                    ColorPalette colorPalette = bitmap.Palette;
                    for (int i = 0; i < 256; i++)
                    {
                        colorPalette.Entries[i] = Color.FromArgb(i, i, i);
                    }
                    bitmap.Palette = colorPalette;

                    converter.OutputPixelFormat = PixelType.Mono8;
                }
                else if (grabResult.PixelTypeValue == PixelType.YUV422packed) //  YUV422Packed
                {
                    bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);

                    converter.OutputPixelFormat = PixelType.BGRA8packed;
                }
                else if (grabResult.PixelTypeValue == PixelType.YUV422_YUYV_Packed) //  YCbCr422
                {
                    bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);

                    converter.OutputPixelFormat = PixelType.BGRA8packed;
                }
                else if (grabResult.PixelTypeValue == PixelType.BayerBG8) //  BG8
                {
                    bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);

                    converter.OutputPixelFormat = PixelType.BGRA8packed;
                }
                else
                {
                    throw new Exception("このプログラムでは対応していないフォーマットです。");
                }

                //  取込画像をビットマップにコピーします。
                if (bitmap != null)
                {
                    BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                    IntPtr ptrBmp = bmpData.Scan0;
                    converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
                    bitmap.UnlockBits(bmpData);
                }

            }
            catch (Exception excep)
            {
                System.Diagnostics.Debug.WriteLine("Bitmap生成中にエラー発生。エラーメッセージ : {0}", excep.Message);
            }
        }

        //  カメラをオープンします。
        private void PortOpen_Click(object sender, EventArgs e)
        {
            try
            {
#if DEBUG
                // デバッグモード時は長めに設定します。
                // 300000ミリ秒（5分）間、無通信状態が続いた場合はタイムアウトします。
                Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "300000");
#else
                Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "3000"); // 3秒
#endif

                // 現在PCに接続されているカメラのデバイスリストを作成します。
                List<ICameraInfo> allDeviceInfos = CameraFinder.Enumerate();

                //デバイスリストよりデバイス数を読み込む
                numDevices = CameraFinder.Enumerate().Count;

                // 接続されているカメラの台数をチェックします。
                if (allDeviceInfos.Count == 0)
                {
                    throw new Exception("カメラが見つかりませんでした。");
                }

                // カメラインスタンスを生成
                //camera0 = new Camera();
                camera0 = new Camera(serial0);

                // コントロールチャンネルのオープンを行います。
                camera0.Open();


                // 画像取込に使用するバッファ数を指定する。
                camera0.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(MaxNumBuffer);


                // ピクセルフォーマットの指定を行います。
                if (usePixeFormat == MyPixelFormat.YUV422PACED) //  YUV422 Packed
                {
                    //  YUV422 Packedのピクセルフォーマットが使用できるかチェック
                    if (camera0.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.YUV422Packed) == false)
                    {
                        throw new Exception("YUV422 Packedのピクセルフォーマットは使用できません。");
                    }
                }
                else if (usePixeFormat == MyPixelFormat.YCbCr422) //  YCbCr422
                {
                    //  YCbCr422のピクセルフォーマットが使用できるかチェック
                    if (camera0.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.YCbCr422_8) == false)
                    {
                        throw new Exception("YCbCr422のピクセルフォーマットは使用できません。");
                    }
                }
                else if (usePixeFormat == MyPixelFormat.MONO8)     //  モノクロ 8bit
                {
                    //  8ビットモノクロのピクセルフォーマットが使用できるかチェック
                    if (camera0.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono8) == false)
                    {
                        throw new Exception("8ビットモノクロのピクセルフォーマットは使用できません。");
                    }
                }
                else if (usePixeFormat == MyPixelFormat.BG8)
                {
                    //  BG8のピクセルフォーマットが使用できるかチェック
                    if (camera0.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.BayerBG8) == false)
                    {
                        throw new Exception("BG8のピクセルフォーマットは使用できません。");
                    }
                }

                ShowStatusMessage("カメラのオープンに成功しました。");

                // 各ボタンを状態を変更します。
                PortOpen.Enabled = false;
                PortClose.Enabled = true;
                Grab.Enabled = true;
                GrabCont.Enabled = true;
                GrabCont1.Enabled = true;
                btPixForm.Enabled = true;

                Display.Image = null;
                Display.Update();


                counter = 0;
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);

                if (camera0 != null)
                {
                    if (camera0.IsOpen)
                    {
                        camera0.Close();
                    }
                    camera0.Dispose();
                    camera0 = null;
                }

                ShowStatusMessage("カメラのオープンに失敗しました。");
            }
        }

        //  カメラクローズ
        private void PortClose_Click(object sender, EventArgs e)
        {
            try
            {
                //  カメラのクローズ
                camera0.Close();
                camera0 = null;


                ShowStatusMessage("カメラのクローズに成功しました。");

                // 各ボタンを状態を変更します。
                PortOpen.Enabled = true;
                PortClose.Enabled = false;
                Grab.Enabled = false;
                GrabCont.Enabled = false;
                GrabCont1.Enabled = false;
                btPixForm.Enabled = false;

                //GrabCont3.Enabled = false;
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);

                camera0 = null;
            }
        }

        void DisplayGrabbedImage(Bitmap bitmap)
        {
            // 直前に表示していたビットマップを取得
            Bitmap prevBmp = (Bitmap)Display.Image;

            // 今回生成したビットマップを表示する
            Display.Image = bitmap;

            // 直前に表示していたビットマップを破棄する
            if (prevBmp != null)
            {
                prevBmp = null;
            }
        }
        void DisplayGrabbedImage2(Bitmap bitmap)
        {
            // 直前に表示していたビットマップを取得
            Bitmap prevBmp = (Bitmap)Display1.Image;

            // 今回生成したビットマップを表示する
            Display1.Image = bitmap;

            // 直前に表示していたビットマップを破棄する
            if (prevBmp != null)
            {
                prevBmp = null;
            }
        }


        void ShowStatusMessage(string message)
        {
            StatusMessage.Text = message;

        }

        // </snippet_auth>
        // <snippet_test>

        //  1枚画像取込
        private void Grab_Click(object sender, EventArgs e)
        {
            try
            {
                /////  取込開始(camera0)  ////
                camera0.StreamGrabber.Start();

                //  取込完了待ち
                // 3000ミリ秒経っても画像が取得できなかった場合はタイムアウトになります。
                IGrabResult grabResult0 = camera0.StreamGrabber.RetrieveResult(3000, TimeoutHandling.ThrowException);
                using (grabResult0)
                {
                    if (grabResult0.GrabSucceeded)
                    {
                        //  ビットマップを生成する
                        CreateBitmap(out bitmap, grabResult0);
                        if (bitmap != null)
                        {
                            //  画像をウィンドウに表示する
                            DisplayGrabbedImage(bitmap);

                            ShowStatusMessage("画像取込に成功しました。(" + (++counter) + ")");
                        }
                        else
                        {
                            throw new Exception("画像取込失敗 - (ビットマップ生成エラー)");
                        }
                    }
                    else
                    {
                        throw new Exception("画像取込失敗 - エラーコード : " + grabResult0.ErrorCode.ToString() + "エラーメッセージ : " + grabResult0.ErrorDescription);
                    }
                }

                //  取込停止(camera0)
                camera0.StreamGrabber.Stop();

            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
                ShowStatusMessage(excep.Message);
            }
        }

        private void GrabCont_Click(object sender, EventArgs e)
        {
            if (GrabThread == null)
            {
                //  連続画像取込：停止⇒開始
                try
                {
                    ThreadStart ContinuousGrabbing = () =>
                    {
                        while (true)
                        {
                            if (GrabStopFlag.WaitOne(0) == true)
                            {
                                break;
                            }

                            try
                            {
                                camera0.StreamGrabber.Start();
                                //  取込完了待ち
                                // 3000ミリ秒経っても画像が取得できなかった場合はタイムアウトになります。
                                IGrabResult grabResult0 = camera0.StreamGrabber.RetrieveResult(3000, TimeoutHandling.ThrowException);
                                //　停止する（ミリ秒）
                                //System.Threading.Thread.Sleep(500);
                                using (grabResult0)
                                {
                                    if (grabResult0.GrabSucceeded)
                                    {
                                        //  ビットマップを生成する
                                        CreateBitmap(out bitmap, grabResult0);
                                        if (bitmap != null)
                                        {
                                            detectwork(out bitmap2, bitmap);
                                            if (InvokeRequired)
                                            {
                                                Invoke((MethodInvoker)delegate { DisplayGrabbedImage(bitmap); });
                                                Invoke((MethodInvoker)delegate { DisplayGrabbedImage2(bitmap2); });
                                                Invoke((MethodInvoker)delegate { ShowStatusMessage("画像取込に成功しました。(" + (++counter) + ")"); });
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("画像取込失敗 - (ビットマップ生成エラー)");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("画像取込失敗 - エラーコード : " + grabResult0.ErrorCode.ToString() + "エラーメッセージ : " + grabResult0.ErrorDescription);
                                    }
                                }

                                camera0.StreamGrabber.Stop();
                            }
                            catch (Exception excep2)
                            {
                                Invoke((MethodInvoker)delegate { ShowStatusMessage(excep2.Message); });
                            }

                        }

                        //  取込停止
                        camera0.StreamGrabber.Stop();
                        GrabThread = null;
                    };

                    // 連続画像取込停止フラグ
                    GrabStopFlag = new AutoResetEvent(false);
                    /*GrabStopFlag = new AutoResetEvent(false);*/

                    // 画像取込スレッド開始
                    GrabThread = new Thread(ContinuousGrabbing);
                    GrabThread.Start();

                    PortOpen.Enabled = false;
                    PortClose.Enabled = false;
                    Grab.Enabled = false;
                    takeSnap.Enabled = true;
                    ExitApp.Enabled = false;
                    GrabCont.Text = "連続取込中\n再度クリックで停止";
                }
                catch (Exception excep1)
                {
                    MessageBox.Show(excep1.Message);
                    ShowStatusMessage(excep1.Message);
                }
            }
            else
            {
                //  連続画像取込：起動中⇒停止

                //  連続画像取込停止指示を出す
                GrabStopFlag.Set();

                PortOpen.Enabled = false;
                PortClose.Enabled = true;
                Grab.Enabled = true;
                ExitApp.Enabled = true;
                takeSnap.Enabled = false;
                GrabCont.Text = "連続取込";

                this.Refresh(); //  表示更新
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GrabCont1_Click(object sender, EventArgs e)
        {
            switch (cameramode)
            {
                case 0:
                    Display.SizeMode = PictureBoxSizeMode.Zoom;
                    Display1.SizeMode = PictureBoxSizeMode.Zoom;
                    Display2.SizeMode = PictureBoxSizeMode.Zoom;

                    cameramode = 1;
                    break;
                case 1:
                    Display.SizeMode = PictureBoxSizeMode.Normal;
                    Display1.SizeMode = PictureBoxSizeMode.Normal;
                    Display2.SizeMode = PictureBoxSizeMode.Normal;
                    cameramode = 2;
                    break;
                case 2:
                    Display.SizeMode = PictureBoxSizeMode.StretchImage;
                    Display1.SizeMode = PictureBoxSizeMode.StretchImage;
                    Display2.SizeMode = PictureBoxSizeMode.StretchImage;
                    cameramode = 3;
                    break;
                case 3:
                    Display.SizeMode = PictureBoxSizeMode.AutoSize;
                    Display1.SizeMode = PictureBoxSizeMode.AutoSize;
                    Display2.SizeMode = PictureBoxSizeMode.AutoSize;
                    cameramode = 4;
                    break;
                case 4:
                    Display1.SizeMode = PictureBoxSizeMode.CenterImage;
                    Display2.SizeMode = PictureBoxSizeMode.CenterImage;
                    Display.SizeMode = PictureBoxSizeMode.CenterImage;
                    cameramode = 0;
                    break;
                default:
                    cameramode = 0;
                    break;
            }
            lblSizeMode.Text = Display.SizeMode.ToString();
        }

        //  アプリケーション終了
        private void ExitApp_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void btPixForm_Click(object sender, EventArgs e)
        {
            switch (pixelformat)
            {
                case 0:
                    usePixeFormat = MyPixelFormat.MONO8;
                    pixelformat = 1;
                    break;
                case 1:
                    usePixeFormat = MyPixelFormat.YCbCr422;
                    pixelformat = 0;
                    break;
                default:
                    pixelformat = 0;
                    break;
            }
            lblPixForm.Text = usePixeFormat.ToString();
            if (GrabThread != null) GrabCont.PerformClick();
            if (PortClose.Enabled == true) PortClose.PerformClick();
            PortOpen.PerformClick();
            Thread.Sleep(100);
            GrabCont.PerformClick();
        }

        private void detectwork(out Bitmap dstbitmap, Bitmap srcbitmap)
        {

            Mat hsvbitmap = BitmapConverter.ToMat(srcbitmap);
            // Convert the image to HSV color space
            Mat hsvImage = new Mat();
            Cv2.CvtColor(hsvbitmap, hsvImage, ColorConversionCodes.BGR2HSV);

            // Define the range of bright orange color in HSV
            Scalar lowerOrange = new Scalar(Convert.ToInt16(RedL.Value), Convert.ToInt16(GreenL.Value), Convert.ToInt16(BlueL.Value));
            Scalar upperOrange = new Scalar(Convert.ToInt16(RedH.Value), Convert.ToInt16(GreenH.Value), Convert.ToInt16(BlueH.Value));

            // Create a binary mask for the bright orange color
            Mat mask = new Mat();
            Cv2.InRange(hsvImage, lowerOrange, upperOrange, mask);
            mask = GeneralClosing(mask, 5, 5);

            // Find contours in the mask
            Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(mask, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // Draw a green contour around the identified object

            foreach (Point[] contour in contours)
            {
                double area = Cv2.ContourArea(contour);
                if (area > 100000)
                {
                    Point[] approx = Cv2.ApproxPolyDP(contour, 0.07 * Cv2.ArcLength(contour, true), true);
                    if (approx.Length == 4)
                    {
                        RotatedRect rect = Cv2.MinAreaRect(approx);
                        //Point2f[] vertices = rect.Points(); // Get the vertices of the rotated rectangle

                        //Point point = new Point((rect.Center.X-(rect.Size.Width/2)), (rect.Center.Y-(rect.Size.Height/2)));
                        //snapPoints.Add(new SnapPoints { StartPoint = point, Width = rect.Size.Width,Height=rect.Size.Height });
                        Rect cropRegion = new Rect();
                        if (rect.Size.Width > rect.Size.Height)
                        {
                            cropRegion = new Rect(Convert.ToInt16(rect.Center.X - (rect.Size.Width / 2)), Convert.ToInt16(rect.Center.Y - (rect.Size.Height / 2)), Convert.ToInt16(rect.Size.Width), Convert.ToInt16(rect.Size.Height));
                        }
                        else
                        {
                            cropRegion = new Rect(Convert.ToInt16(rect.Center.X - (rect.Size.Height / 2)), Convert.ToInt16(rect.Center.Y - (rect.Size.Width / 2)), Convert.ToInt16(rect.Size.Height), Convert.ToInt16(rect.Size.Width));
                        }

                        if (_snaps == true)
                        {
                            if (_motor == true)
                            {
                                Mat croppedImage = new Mat(hsvbitmap, cropRegion);
                                lookforscracths(croppedImage);
                                _motor = false;
                                _snapscount++;
                            }
                        }

                        Cv2.Rectangle(hsvbitmap, cropRegion, Scalar.Lime, 2);
                        /*for (int i = 0; i < 4; i++)
                        {
                            Cv2.Line(hsvbitmap, (Point)vertices[i], (Point)vertices[(i + 1) % 4], new Scalar(0, 255, 0), 4);

                        }*/

                        string texto = rect.Center.ToString() + ", Width: " + ((int)rect.Size.Width).ToString() + ", Height: " + ((int)rect.Size.Height).ToString();
                        Cv2.PutText(hsvbitmap, texto, new Point((int)rect.Center.X - 300, (int)rect.Center.Y), HersheyFonts.HersheySimplex, 1, new Scalar(0, 255, 0), 2);
                        break;
                    }

                }
            }
            bitmap = BitmapConverter.ToBitmap(hsvbitmap);
            dstbitmap = BitmapConverter.ToBitmap(mask);
        }
        public Mat GeneralClosing(Mat src, int kernelsize, int times)
        {
            for (int i = 0; i < times; i++)
            {
                Mat dest = new Mat();
                Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(kernelsize, kernelsize));
                dest = src.Dilate(kernel);//ダイレート処理                
                src = dest;
            }

            for (int i = 0; i < times; i++)
            {
                Mat dest = new Mat();
                Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(kernelsize, kernelsize));
                dest = src.Erode(kernel);
                src = dest;
            }

            return src;
        }

        private void takeSnap_Click(object sender, EventArgs e)
        {
            _snaps = true;
            takeSnap.Enabled = false;
            btMotor.Enabled = true;
        }

        private void lookforscracths(Mat srcImage)
        {
            if (srcImage == null)
            {
                MessageBox.Show("Please load an image first!");
                return;
            }
            Mat _markedImage = srcImage.Clone();
            int contador = 0;
            PickupColorHsvMask(srcImage, 0, 359, 255, 0, 255, 235);
            Point[][] contours;
            HierarchyIndex[] hierarchyIndices;
            Cv2.FindContours(_hsvImage, out contours, out hierarchyIndices, RetrievalModes.List, ContourApproximationModes.ApproxSimple);


            foreach (Point[] contour in contours)
            {
                int UpX = 0;
                int UpY = 100000;
                int DnX = 100000;
                int DnY = 0;

                foreach (Point point in contour)
                {
                    if ((point.X < (_hsvImage.Width / 4)) || (point.X > (_hsvImage.Width * 3 / 4)))
                    {
                        goto nextpoint;
                    }
                    if (point.X > UpX) UpX = point.X;
                    if (point.X < DnX) DnX = point.X;
                    if (point.Y > DnY) DnY = point.Y;
                    if (point.Y < UpY) UpY = point.Y;

                }
                double area = Cv2.ContourArea(contour);
                if (area >= 0 && area < 1000)
                {
                    Point[] approx = Cv2.ApproxPolyDP(contour, 0.04 * Cv2.ArcLength(contour, true), true);
                    if (approx.Length < 5)
                    {
                        Rect rect = new Rect(DnX, UpY, (UpX - DnX), (DnY - UpY));
                        Cv2.Rectangle(_markedImage, rect, new Scalar(255, 0, 0), 1);
                        Point center = new Point((UpX + DnX) / 2, (UpY + DnY) / 2);
                        centers.Add(center);
                        //Cv2.ImShow("cc", _markedImage);
                        //Cv2.WaitKey();
                        contador++;
                    }
                }
            nextpoint:
                Console.Write("");
            }
            int maisconexoes = 0;
            int primeiro = 0;
            int ultimo = 0;
            if (contador >= 3)
            {

                for (int x = 0; x < contador - 2; x++)
                {
                    for (int k = x; k < contador - 2; k++)
                    {
                        int ultimoconcectado = 0;
                        double angulo2 = 0;

                        int j = k;
                        int conectados = 2;
                    increaseJ:
                        j = j + 1;
                        double angulo = Calcangulo(x, j);

                        if (angulo < 10 && angulo > -10)
                        {
                            if (j == k + 1)
                            {
                                angulo2 = angulo;
                                goto increaseJ;
                            }
                            if ((angulo < angulo2 + 2) && (angulo > angulo2 - 2))
                            {
                                conectados++;
                                ultimoconcectado = j;

                            }
                            if (j < contador - 1) goto increaseJ;
                            if (conectados > maisconexoes)
                            {
                                maisconexoes = conectados;
                                primeiro = x;
                                ultimo = ultimoconcectado;
                            }
                        }
                    }
                }

                int mediaX = (centers[primeiro].X + centers[ultimo].X) / 2;
                Point linhabase = new Point(mediaX, _markedImage.Height);
                Point linhaalta = new Point(mediaX, 0);
                double lineangle = 0;
                if (maisconexoes > 2)
                {
                    Cv2.Line(_markedImage, linhabase, linhaalta, Scalar.Black, 4);
                    double arcsinvalue = (2 * mediaX) - _markedImage.Width;
                    arcsinvalue = arcsinvalue / _markedImage.Width;
                    lineangle = Math.Asin(arcsinvalue) * (180 / Math.PI) + (_snapscount * 30);
                    //Cv2.PutText(_markedImage, lineangle.ToString(), new Point(mediaX, _markedImage.Height / 2), HersheyFonts.HersheySimplex, 1, Scalar.Aqua);
                    //label1.Text = Math.Round(lineangle, 2).ToString();
                    //lblconexoes.Text = maisconexoes.ToString();
                }
                if (_selectedconnection <= maisconexoes)
                {
                    _selectedconnection = maisconexoes;
                    _selectedangle = Math.Round(lineangle, 2);
                    _selectedframe = _markedImage;
                }
                //Cv2.Line(srcImage, centers[primeiro], centers[ultimo], Scalar.Green, 4);



            }
            centers.Clear();
            //Cv2.Circle(_processedImage, new Point(125, 135), 3, new Scalar(0, 0, 255), -1);
            //Vec3b colour = _originalImage.At<Vec3b>(125, 135);
            //Vec3b colour2 = _originalImage.At<Vec3b>(100, 100);

            //if (colour.Item0 == 255 && colour.Item1 == 255 && colour.Item2 == 255)
            //label1.Text = colour.ToString() + "   " + colour2.ToString();
            Display2.Image = _markedImage.ToBitmap();
            return;
        }

        public double Calcangulo(int x, int y)
        {
            double xdiff = centers[x].X - centers[y].X;
            double ydiff = centers[x].Y - centers[y].Y;
            double angulo = Math.Atan2(xdiff, ydiff) * 180.0 / Math.PI; ;
            return angulo;
        }

        public void PickupColorHsvMask(Mat originalImage, int hStart, int hEnd, int sMax, int sMin, int vMax, int vMin)
        {
            Mat src = originalImage.Clone();

            // BGRからHSVへ変換
            Mat outputImage = new Mat();

            //hsvImage = src.CvtColor(ColorConversionCodes.BGR2HSV_FULL, 3);
            Mat hsvImage = src.CvtColor(ColorConversionCodes.BGR2HSV, 3);

            // HSV変換画像を保存

            // inRangeを用いてフィルタリング
            int startH = hStart;
            int endH = (hEnd >= 360) ? 359 : hEnd;

            Scalar minScalarL = new Scalar(startH / 2, sMin, vMin);
            Scalar maxScalarL = new Scalar(endH / 2, sMax, vMax);
            Mat maskImage = hsvImage.InRange(minScalarL, maxScalarL);

            if (hEnd >= 360)
            {
                startH = 0;
                endH = hEnd - 360;

                Scalar minScalarH = new Scalar(startH / 2, sMin, vMin);
                Scalar maxScalarH = new Scalar(endH / 2, sMax, vMax);
                Mat maskImageH = hsvImage.InRange(minScalarH, maxScalarH);

                maskImageH.CopyTo(maskImage, maskImageH);
            }

            // マスク画像を保存

            // マスクを基に入力画像をフィルタリング
            src.CopyTo(outputImage, maskImage);

            // 結果の保存
            _hsvImage = maskImage;
        }

        private void btMotor_Click(object sender, EventArgs e)
        {
            _motor = true;
            if (_snapscount > 11)
            {
                _snaps = false;
                _snapscount = 0;
                takeSnap.Enabled = true;
                btMotor.Enabled = false;
            }
        }

    }
}