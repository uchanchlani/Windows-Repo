using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KinectStreams
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members

        Mode _mode = Mode.Color;
        string path_ = "";
        bool written_ = false;

        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        IList<Body> _bodies;

        bool _displayBody = false;
        int idx = 0;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Event handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                _sensor.Open();

                //_reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                //_reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
                _sensor.BodyFrameSource.OpenReader().FrameArrived += this.Body_Reader_FrameArrived;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
        }
        void Body_Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            written_ = false;
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {


                    _bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(_bodies);

                    foreach (var body in _bodies)
                    {
                        if (body != null)
                        {
                            if (body.IsTracked)
                            {
                                string s = "";
                                foreach (Joint joint in body.Joints.Values)
                                {
                                    s = s + joint.Position.X + " " + joint.Position.Y + " " + joint.Position.Z + "\n";
                                }

                                Console.WriteLine("I m here " + idx.ToString());
                                System.IO.File.WriteAllText(path_ + @"/Skeletonframe" + idx.ToString() + ".txt", s);
                                written_ = true;
//                                idx++;
                                // Draw skeleton.
                                //if (_displayBody)
                                //{
                                //    canvas.DrawSkeleton(body);
                                //}
                            }
                        }
                    }
                }
            }

            if(written_ == false)
                System.IO.File.WriteAllText(path_ + @"/Skeletonframe" + idx.ToString() + ".txt", "");
            idx++;
        }


        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();

            // Color
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Color)
                    {
                        camera.Source = frame.ToBitmap();
                    }
                }
            }

            // Depth
            using (var frame = reference.DepthFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Depth)
                    {
                        camera.Source = frame.ToBitmap();
                    }
                }
            }

            // Infrared
            using (var frame = reference.InfraredFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Infrared)
                    {
                        camera.Source = frame.ToBitmap();
                    }
                }
            }

            // Body
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    canvas.Children.Clear();

                    _bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(_bodies);

                    foreach (var body in _bodies)
                    {
                        if (body != null)
                        {
                            if (body.IsTracked)
                            {
                                // Draw skeleton.
                                if (_displayBody)
                                {
                                    canvas.DrawSkeleton(body);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Color;
        }

        private void Depth_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Depth;
        }

        private void Infrared_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Infrared;
        }

        private void Body_Click(object sender, RoutedEventArgs e)
        {
            _displayBody = !_displayBody;
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            path_ = this.TextInput.Text;
            if (path_ == "") return;
            Console.WriteLine(path_);
            Button.Visibility = System.Windows.Visibility.Hidden;
            Label.Visibility = System.Windows.Visibility.Hidden;
            TextInput.Visibility = System.Windows.Visibility.Hidden;
            BrowseButton.Visibility = System.Windows.Visibility.Hidden;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderbrowser = new System.Windows.Forms.FolderBrowserDialog();

            folderbrowser.ShowDialog();
            TextInput.Text = folderbrowser.SelectedPath;
            /*Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "";
            openFileDialog1.Filter = "All folders (*)|*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            openFileDialog1.ShowDialog();
                TextInput.Text = openFileDialog1.FileName;
            */
        }
    }

    public enum Mode
    {
        Color,
        Depth,
        Infrared
    }
}
