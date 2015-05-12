using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace KinectMLConnect
{
    public partial class KinectToMatlab : Form
    {
        #region Members

        int idx = 0;
        int idx1 = 0;
        int checkbox_;
        int min_, max_;
        string path_;
        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Reader for depth frames
        /// </summary>
        private DepthFrameReader depthFrameReader = null;

        /// <summary>
        /// Description of the data contained in the depth frame
        /// </summary>
        private FrameDescription depthFrameDescription = null;

        public ushort[, ] depthFrameDataBuffer;
        /// Intermediate storage for frame data
        /// </summary>
        public ushort[] depthFrameData = null;



        /// <summary>
        /// Reader for IR frames
        /// </summary>
        private InfraredFrameReader IRFrameReader = null;

        /// <summary>
        /// Description of the data contained in the depth frame
        /// </summary>
        private FrameDescription IRFrameDescription = null;

        /// <summary>
        /// Intermediate storage for frame data converted to color
        /// </summary>
        public ushort[] IRFrameData = null;

        private ColorFrameReader colorFrameReader = null;

        /// <summary>
        /// Description of the data contained in the depth frame
        /// </summary>
        private FrameDescription colorFrameDescription = null;

        /// <summary>
        /// Intermediate storage for frame data
        /// </summary>
        public byte[] colorFrameData=null;


        /// <summary>
        /// Current status text to display
        /// </summary>
        private string textBoxText = null;

        /// <summary>
        /// Keep track of the frames
        /// </summary>
        int frameCount=0;

        /// <summary>
        /// writes to output files
        /// </summary>
        private MATWriter matfw = null;

        /// <summary>
        /// String path to save the framefiles
        /// </summary>
        private string filePath = null;

        /// <summary>
        /// Timing of the frames
        /// </summary>
        private long[] timing = null;

        /// <summary>
        /// Control which type of data to recieve
        /// </summary>
        private string extractType = null;
        ImageSource[] arrayOfColorFrame = null; 

        #endregion

        /// <summary>
        /// Calling the constructor
        /// </summary>
        public KinectToMatlab()
        {
            // get the kinectSensor object
            this.kinectSensor = KinectSensor.GetDefault();

            // open the reader for the depth frames
            this.depthFrameReader = this.kinectSensor.DepthFrameSource.OpenReader();

            // Open IR reader for IR frames
            this.IRFrameReader = this.kinectSensor.InfraredFrameSource.OpenReader();

            // open the reader for the rgb frames
            this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();

            // wire handler for frame arrival
            this.IRFrameReader.FrameArrived += this.IR_Reader_FrameArrived;
            this.depthFrameReader.FrameArrived += this.Depth_Reader_FrameArrived;
            this.colorFrameReader.FrameArrived += this.Color_Reader_FrameArrived;

            // get FrameDescription from DepthFrameSource
            this.depthFrameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;

            // get FrameDescription from InfraredFrameSOurce
            this.IRFrameDescription = this.kinectSensor.InfraredFrameSource.FrameDescription;

            // allocate space to put the pixels being received and converted
            this.depthFrameData = new ushort[this.depthFrameDescription.Width * this.depthFrameDescription.Height];

            // allocate space for the buffer
            this.depthFrameDataBuffer = new ushort[150, this.depthFrameDescription.Width * this.depthFrameDescription.Height];

            // allocate space for IR frame
            this.IRFrameData = new ushort[this.IRFrameDescription.Width * this.IRFrameDescription.Height];

            // set IsAvailableChanged event notifier
            this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;

            // open the sensor
            this.kinectSensor.Open();

            // initialize the components (controls) of the window
            this.InitializeComponent();

            // set system initial status
            this.StatusText = Properties.Resources.InitialStatusText;

            // framecount is zero
            this.frameCount = 0;

            // Create output directory
            System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Kinect to MatLab");

            // Choose options
            this.DepthRadio.CheckedChanged += new EventHandler(Options_CheckedChanged);
            this.IRRadio.CheckedChanged += new EventHandler(Options_CheckedChanged);

            // Run option
            this.extractType = "Color";

            // Allocate timing table
            this.timing = new long[10000];

            arrayOfColorFrame = new ImageSource[1100];
        }

        /// <summary>
        /// Handles the IR frame data arriving from the sensor
        /// </summary>
        private void IR_Reader_FrameArrived(object sender, InfraredFrameArrivedEventArgs e)
        {
            /*if (this.StatusText == Properties.Resources.RunningStatusText)
            {
                if (this.extractType == "IR")
                {
                    using (InfraredFrame IRFrame = e.FrameReference.AcquireFrame())
                    {
                        if (IRFrame != null)
                        {
                            // the fastest way to process the body index data is to directly access 
                            // the underlying buffer
                            using (Microsoft.Kinect.KinectBuffer IRbuffer = IRFrame.LockImageBuffer())
                            {

                                {
                                    IRFrame.CopyFrameDataToArray(IRFrameData);
                                    this.frameCount++;
                                    this.timing[frameCount] = (ushort)IRFrame.RelativeTime.Milliseconds;
                                    filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Kinect to MatLab" + "/IRframe" + frameCount.ToString() + ".MAT";
                                    this.matfw = new MATWriter("IRmat", filePath, IRFrameData, IRFrame.FrameDescription.Height, IRFrame.FrameDescription.Width);

                                }
                            }
                        }
                    }
                }
            }
            else
            {
                SaveParamsToFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Kinect to MatLab" + "/Intrinsic parameters.txt");
                this.StatusText = Properties.Resources.SensorIsAvailableStatusText;
            }*/
        }

        /// <summary>
        /// Chnage options based on radio buttons
        /// </summary>
        private void Options_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (DepthRadio.Checked)
            {
                this.extractType = "Depth";
                this.IRRadio.Checked = false;
            }
            else if (IRRadio.Checked)
            {
                this.extractType = "IR";
                this.DepthRadio.Checked = false;
            }
        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Console.WriteLine("i M HERE");
            for (int i = 0; i < idx; i++)
            {
                System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                img.Source = arrayOfColorFrame[i];
                //WriteableBitmap colorImage = new WriteableBitmap(640, 480, 96, 96, PixelFormats.Bgr32, null);
                //colorImage.WritePixels(new Int32Rect(0, 0, colorImage.PixelWidth, colorImage.PixelHeight), pixels1, colorImage.PixelWidth * 4, 0);
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(System.Windows.Media.Imaging.BitmapFrame.Create(img.Source as System.Windows.Media.Imaging.BitmapSource)));

                //path = System.IO.Path.Combine("C:/", "Kinected", "Images");

                try
                {

                    using (System.IO.FileStream fs = new System.IO.FileStream(@"G:\_Hand_Gestures\Color\color_image" + i.ToString() + ".jpg", System.IO.FileMode.Create))
                    {
                        encoder.Save(fs);
                        Console.WriteLine("Image Successfully Saved...");
                        
                    }

                }
                catch (System.IO.IOException)
                {
                    System.Windows.MessageBox.Show("Save Failed");
                }
            }
                if (this.depthFrameReader != null)
                {
                    // DepthFrameReader is IDisposable
                    this.depthFrameReader.Dispose();
                    this.depthFrameReader = null;
                }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
        }

        public static ImageSource ToBitmap(ColorFrame frame)
        {
            int width = frame.FrameDescription.Width;
            int height = frame.FrameDescription.Height;
            PixelFormat format = PixelFormats.Bgr32;

            byte[] pixels = new byte[width * height * ((format.BitsPerPixel + 7) / 8)];

            if (frame.RawColorImageFormat == ColorImageFormat.Bgra)
            {
                frame.CopyRawFrameDataToArray(pixels);
            }
            else
            {
                frame.CopyConvertedFrameDataToArray(pixels, ColorImageFormat.Bgra);
            }

            int stride = width * format.BitsPerPixel / 8;

            return BitmapSource.Create(width, height, 96, 96, format, null, pixels, stride);
        }

        /// <summary>
        /// Handles the depth frame data arriving from the sensor
        /// </summary>
        private void Depth_Reader_FrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            if (this.StatusText == Properties.Resources.RunningStatusText)
            {
                //if(this.extractType == "Depth")
                {
                    using (DepthFrame depthFrame = e.FrameReference.AcquireFrame())
                    {
                        if (depthFrame != null)
                        {
                            // the fastest way to process the body index data is to directly access 
                            // the underlying buffer
                            using (Microsoft.Kinect.KinectBuffer depthBuffer = depthFrame.LockImageBuffer())
                            {
                            
                                {
                                    if ((checkbox_ == 2 && frameCount>=min_) || (checkbox_ == 1 && frameCount>=max_))
                                        Application.Exit();
                                    //Write timings to txt File
                                    //Change 1221 to total number of frames - 1 and output path of file 
                                    if (checkbox_ == 2 && this.frameCount == min_)
                                    {
                                        this.timing[this.frameCount++] = (ushort)depthFrame.RelativeTime.Milliseconds;
                                        string s = "";
                                        for (int i = 0; i < frameCount; i++) s = s + timing[i].ToString() + "\n";
                                        System.IO.File.WriteAllText(path_ + @"\Depth_Timings.txt", s);
                                    }

                                    depthFrame.CopyFrameDataToArray(depthFrameData);
                                    //this.frameCount++;
                                    this.timing[this.frameCount++] = (ushort)depthFrame.RelativeTime.Milliseconds;
                                    int length_ = depthFrameData.Length;
//                                    for (int i = 0; i < depthFrameData.Length; i++)
//                                        depthFrameDataBuffer[0, i] = depthFrameData[i];
                                    if (checkbox_ == 1 && frameCount >= min_ && frameCount < max_)
                                    {
                                        Console.WriteLine("here " + frameCount.ToString());                                        
                                        for (int i = 0; i < length_; i++)
                                            depthFrameDataBuffer[idx, i] = depthFrameData[i];
                                        idx++;
                                    }
                                    else if (checkbox_ == 1 && frameCount == max_)
                                    {
                                        Console.WriteLine("here " + frameCount.ToString());
                                        for (int j = 0; j < idx; j++)
                                        {
                                            for (int i = 0; i < length_; i++)
                                                depthFrameData[i] = depthFrameDataBuffer[j, i];
                                            filePath = path_ + @"\Depthframe" + (j+min_).ToString() + ".MAT";
                                            this.matfw = new MATWriter("depthmat", filePath, depthFrameData, depthFrame.FrameDescription.Height, depthFrame.FrameDescription.Width);
                                        }
                                        Application.Exit();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                SaveParamsToFile(path_ + @"\Intrinsic parameters.txt");
                this.StatusText = Properties.Resources.SensorIsAvailableStatusText;
            }

        }

        void writeColorFrames()
        {
            for (int i = 0; i < idx1; i++)
            {
                System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                img.Source = arrayOfColorFrame[i];
                //WriteableBitmap colorImage = new WriteableBitmap(640, 480, 96, 96, PixelFormats.Bgr32, null);
                //colorImage.WritePixels(new Int32Rect(0, 0, colorImage.PixelWidth, colorImage.PixelHeight), pixels1, colorImage.PixelWidth * 4, 0);
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(System.Windows.Media.Imaging.BitmapFrame.Create(img.Source as System.Windows.Media.Imaging.BitmapSource)));

                //path = System.IO.Path.Combine("C:/", "Kinected", "Images");

                try
                {

                    using (System.IO.FileStream fs = new System.IO.FileStream(@"G:\01\color_data\color_image" + (idx - idx1 + i).ToString() + ".jpg", System.IO.FileMode.Create))
                    {
                        encoder.Save(fs);
                        Console.WriteLine("Image Successfully Saved...");

                    }

                }
                catch (System.IO.IOException)
                {
                    System.Windows.MessageBox.Show("Save Failed");
                }
            }
            Application.Exit();
        
        }

        /// <summary>
        /// Handles the color frame data arriving from the sensor
        /// </summary>
        private void Color_Reader_FrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            /*if (this.StatusText == Properties.Resources.RunningStatusText)
            {
                //if (this.extractType == "Color")
                {
                    using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
                    {
                        if (colorFrame != null)
                        {
                            // change here
                            if (idx >= 563)
                            {
                                arrayOfColorFrame[idx1++] = ToBitmap(colorFrame);idx++;
                                writeColorFrames();
                            }
                            // the fastest way to process the body index data is to directly access 
                            // the underlying buffer
                            Console.WriteLine("I m here "+idx.ToString());
                            //idx++;
                            using (Microsoft.Kinect.KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                                {

                                    //Console.WriteLine("I m here");
                                    //Code starts here
                                    //System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                                    //img.Source = ToBitmap(colorFrame);
                                    //WriteableBitmap colorImage = new WriteableBitmap(640, 480, 96, 96, PixelFormats.Bgr32, null);
                                    //colorImage.WritePixels(new Int32Rect(0, 0, colorImage.PixelWidth, colorImage.PixelHeight), pixels1, colorImage.PixelWidth * 4, 0);
                                    //BitmapEncoder encoder = new JpegBitmapEncoder();
                                    //encoder.Frames.Add(BitmapFrame.Create(System.Windows.Media.Imaging.BitmapFrame.Create(img.Source as System.Windows.Media.Imaging.BitmapSource)));

                                    //path = System.IO.Path.Combine("C:/", "Kinected", "Images");

                                    //try
                                    //{

                                    //    using (System.IO.FileStream fs = new System.IO.FileStream(@"C:\Users\Kinect Lab\Desktop\color_image" + idx.ToString() + ".jpg", System.IO.FileMode.Create))
                                    //    {
                                    //        encoder.Save(fs);
                                    //        Console.WriteLine("Image Successfully Saved...");
                                    //        idx++;
                                    //    }

                                    //}
                                    //catch (System.IO.IOException)
                                    //{
                                    //    System.Windows.MessageBox.Show("Save Failed");
                                    //}
                                    // TODO: UNCOMMENT THESE LINES
                                    //if(idx>=450 && idx<600)  arrayOfColorFrame[idx1++] = ToBitmap(colorFrame);
                                    //idx++;

                                    //change 1221 to total number of frames-1
                                    if (frameCount == 543)
                                    {
                                        this.timing[frameCount++] = (ushort)colorFrame.RelativeTime.Milliseconds;
                                        string s = "";
                                        for (int i = 0; i < frameCount; i++) s = s + timing[i].ToString() + "\n";
                                        System.IO.File.WriteAllText(@"G:\01\color_data\Color_Timings.txt", s);
                                    }
                                    this.timing[frameCount++] = colorFrame.RelativeTime.Milliseconds;
                                    Console.WriteLine(frameCount);
                                }
                            
                        }
                    }
                }
            }
            else
            {
                SaveParamsToFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Kinect to MatLab" + "/Intrinsic parameters.txt");
                this.StatusText = Properties.Resources.SensorIsAvailableStatusText;
            }
             */
        }

        /*private void Color_Reader_FrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            if (this.StatusText == Properties.Resources.RunningStatusText)
            {

                {
                    using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
                    {
                        if (colorFrame != null)
                        {
                            //using (Microsoft.Kinect.KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                                {
                                    {
                                        byte x = 8;
                                        ushort y = (ushort)x;
                                        Console.WriteLine("I m ushort" + y);
                                        Console.WriteLine("I m here"+idx.ToString());
                                        colorFrameData = new byte[((PixelFormats.Bgr32.BitsPerPixel+7) / 8) * colorFrame.FrameDescription.Width * colorFrame.FrameDescription.Height];
                                        Console.WriteLine("I m here" + colorFrame.FrameDescription.BytesPerPixel + "," + colorFrame.FrameDescription.Width +","+ colorFrame.FrameDescription.Height);
                                        Console.WriteLine("I m here" + PixelFormats.Bgr32.BitsPerPixel/8);
                                        
                                        //colorFrame.CopyRawFrameDataToArray(colorFrameData);
                                        if (colorFrame.RawColorImageFormat == ColorImageFormat.Bgra)
                                        {
                                            colorFrame.CopyRawFrameDataToArray(colorFrameData);
                                        }
                                        else
                                        {
                                            colorFrame.CopyConvertedFrameDataToArray(colorFrameData, ColorImageFormat.Bgra);
                                       
                                        }
                                        Console.WriteLine("I m here555" + colorFrameData.Length);
                                        frameCount++;
                                            
                                            this.timing[frameCount] = (ushort)colorFrame.RelativeTime.Milliseconds;
                                            filePath = "G:\\_Hand_Gestures\\HasthaMudra1\\Color_Mat\\Colorframe" + frameCount.ToString() + ".MAT";
                                            ushort[] newColorFrameData = new ushort[colorFrame.FrameDescription.Width * colorFrame.FrameDescription.Height];
                                            int cnt = 0;
                                            for (int i = 0; i < ((PixelFormats.Bgr32.BitsPerPixel) / 8) * colorFrame.FrameDescription.Width * colorFrame.FrameDescription.Height; i+=4)
                                            {
                                              
                                                newColorFrameData[cnt++] = (ushort)colorFrameData[i];
                                                                                       
                                            }
                                        
                                        /*ushort[] newColorFrameData=new ushort[(colorFrameData.Length>>1)];
                                            for (ushort objIndex = 0; objIndex < newColorFrameData.Length; ++objIndex)
                                            {
                                                ushort length = sizeof(UInt16);
                                                newColorFrameData[objIndex] = (ushort)((ushort)(colorFrameData[objIndex * length] << (ushort)8) +
                                                                                         colorFrameData[objIndex + 1]);
                                            }
                                            Console.WriteLine("I m here%%%%%" + colorFrameData.Length);
                                            ushort[] newColorFrameData=new ushort[colorFrameData.Length];
                                            for (ushort objIndex = 0; objIndex < newColorFrameData.Length-1; ++objIndex)
                                            {
                                                Console.WriteLine("I m here%%%%%" + objIndex);
                                                newColorFrameData[objIndex] = BitConverter.ToUInt16(colorFrameData, objIndex);
                                            }
                                            Console.WriteLine("I m here2" + colorFrameData.Length);
                                            this.matfw = new MATWriter( "colormat",filePath, newColorFrameData, colorFrame.FrameDescription.Height, colorFrame.FrameDescription.Width);
                                            Console.WriteLine("I m here3" + colorFrameData.Length);    
                                        //frameCount++;
                                    }
                                    
                                }
                            if (frameCount == 1221)
                            {
                                this.timing[frameCount++] = (ushort)colorFrame.RelativeTime.Milliseconds;
                                string s = "";
                                for (int i = 0; i < frameCount; i++) s = s + timing[i].ToString() + "\n";
                                System.IO.File.WriteAllText(@"G:\_Hand_Gestures\HasthaMudra1\Color_Mat\Color_Timings.txt", s);
                            }
                            this.timing[frameCount++] = colorFrame.RelativeTime.Milliseconds;
                            Console.WriteLine(frameCount);
                        }
                    }
                }
            }
            else
            {
                SaveParamsToFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Kinect to MatLab" + "/Intrinsic parameters.txt");
                this.StatusText = Properties.Resources.SensorIsAvailableStatusText;
            }
        }*/


        /// <summary>
        /// Actions on startup
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            StopButton.Enabled = false;
            StartButton.Enabled = false;
            DepthRadio.Checked = true;
            Options.Visible = false;
            browseButton.Visible = submitButton.Visible = false;
            label1.Visible = label2.Visible = label3.Visible = false;
            pathText.Visible = minText.Visible = maxText.Visible = false;
            depthImage.Checked = depthFrame.Checked = false;
        }

        /// <summary>
        /// Actions on Start button click
        /// </summary>
        private void StartButton_Click(object sender, EventArgs e)
        {
            this.StatusText = Properties.Resources.RunningStatusText;
            StartButton.Enabled = false;
            StopButton.Enabled = true;
        }

        /// <summary>
        /// Actions on Stop button click
        /// </summary>
        private void StopButton_Click(object sender, EventArgs e)
        {
            StopButton.Enabled = false;
            StartButton.Enabled = true;
            this.StatusText = Properties.Resources.StoppedStatusText;
            
           // this.matfw = new MATWriter("timing", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Kinect to MatLab" + "/FrameTimings.mat", this.timing, this.timing.Length, 1);
            
            this.frameCount = 0;
            System.Array.Clear(this.timing, 0, this.timing.Length);
        }

        /// <summary>
        /// Listener for sensor availability
        /// </summary>
        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            if(e.IsAvailable == true)
            {
                this.StatusText = Properties.Resources.SensorIsAvailableStatusText;
            }
            else
            {
                this.StatusText = Properties.Resources.NoSensorStatusText;
            }
        }

        /// <summary>
        /// Listener for status changes (updates the status text)
        /// </summary>
        public string StatusText
        {
            get { return textBoxText; }
            set
            {
                textBoxText = value;
                {
                    this.StatusBox.Text = textBoxText;
                }
            }
        }

        /// <summary>
        /// Extracts the intrinsic parameters of the camera, to enable 3D point clouds
        /// </summary>
        private void SaveParamsToFile(string filePath)
        {
            // Get all the intrinsic parameters
            float Hfov = this.kinectSensor.DepthFrameSource.FrameDescription.HorizontalFieldOfView;
            float Vfov = this.kinectSensor.DepthFrameSource.FrameDescription.VerticalFieldOfView;
            float Dfov = this.kinectSensor.DepthFrameSource.FrameDescription.DiagonalFieldOfView;
            CameraIntrinsics camIntrinsics = this.kinectSensor.CoordinateMapper.GetDepthCameraIntrinsics();
            float Vfl = camIntrinsics.FocalLengthY;
            float Hfl = camIntrinsics.FocalLengthX;
            float Hpp = camIntrinsics.PrincipalPointX;
            float Vpp = camIntrinsics.PrincipalPointY;
            float Rdist = camIntrinsics.RadialDistortionSecondOrder;

            System.IO.File.WriteAllText(filePath,
                "Horizontal Field of View is: " + Hfov.ToString() + "\r\n" +
                "Vertical Field of View is: " + Vfov.ToString() + "\r\n" +
                "Diagonal Field of View is: " + Dfov.ToString() + "\r\n" +
                "Horizontal focal length is: " + Hfl.ToString() + "\r\n" + 
                "Vertical focal length is: " + Vfl.ToString() + "\r\n" +
                "Horizontal principle point is: " + Hpp.ToString() + "\r\n" +
                "Vertical Principal point is: " + Vpp.ToString() + "\r\n" +
                "Radial distortion Second order term is: "+ Rdist.ToString() + "\r\n");



        }

        private void depthImage_CheckedChanged(object sender, EventArgs e)
        {
            if (depthImage.Checked == false) return;
            depthFrame.Visible = depthImage.Visible = false;
            label1.Visible = label2.Visible = label3.Visible = true;
            pathText.Visible = minText.Visible = maxText.Visible = true;
            browseButton.Visible = submitButton.Visible = true;
            checkbox_ = 1;
        }

        private void depthFrame_CheckedChanged(object sender, EventArgs e)
        {
            if (depthFrame.Checked == false) return;
            depthFrame.Visible = depthImage.Visible = false;
            label2.Text = "No of Frames";
            label1.Visible = label2.Visible = true;
            pathText.Visible = minText.Visible = true;
            browseButton.Visible = submitButton.Visible = true;
            checkbox_ = 2;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (checkbox_ == 1)
            {
                if (pathText.Text == "") return;
                if (minText.Text == "") return;
                if (maxText.Text == "") return;
                path_ = pathText.Text;
                min_ = Convert.ToInt16(minText.Text);
                max_ = Convert.ToInt16(maxText.Text);
                Console.WriteLine(path_);
            }
            else if (checkbox_ == 2)
            {
                if (pathText.Text == "") return;
                if (minText.Text == "") return;
                path_ = pathText.Text;
                min_ = Convert.ToInt16(minText.Text);
                Console.WriteLine(path_);
            }
            StartButton.Enabled = true;
            label1.Visible = label2.Visible = label3.Visible = false;
            pathText.Visible = minText.Visible = maxText.Visible = false;
            browseButton.Visible = submitButton.Visible = false;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderbrowser = new System.Windows.Forms.FolderBrowserDialog();

            folderbrowser.ShowDialog();
            pathText.Text = folderbrowser.SelectedPath;
        }

    }
}


