using log4net;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using WpfApp3.src;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp3
{
    /// <summary>
    /// Create and control user app
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog log = 
            LogManager.GetLogger(type: MethodBase.GetCurrentMethod()?.DeclaringType);

        /// temp for prepare click or double click in mouse button
        private readonly System.Windows.Threading.DispatcherTimer _timer = new();
        
        /// button pressed in mouse
        private int button = 0;

        /// <summary>
        /// Constructor. Prepare board of app and controls of user interactions
        /// </summary>
        public MainWindow()
        {
            log4net.Config.XmlConfigurator.Configure();

            // prepare page in xaml
            InitializeComponent();
            log.Info("Init Window");

            // get user data and prepare the image control
            Data.Start();

            // start tempore for mouse
            log.Info("Prepare interval to click mouse");
            _timer.Interval = TimeSpan.FromSeconds(0.2); //wait for the other click for 200ms
            _timer.Tick += Timer_Tick;

            // show message to view user data
            String msg = "LastPath: " + Data.LastPath + "\n";
            msg += "Images: {" + "\n";
            foreach (string s in Data.Images)
            {
                msg += "  " + s + ", ";
            }
            msg += "\n]\n";
            msg += "ImageIndex: " + Data.ImageIndex + "\n";

            msg += "Comics: {" + "\n";
            foreach (string s in Data.Comics)
            {
                msg += "  " + s + ", ";
            }
            msg += "\n]\n";
            msg += "ComicIndex: " + Data.ComicIndex + "\n";

            MessageBox.Show(msg);

        }
        /// <summary>
        /// Method to control click for mouse (if click != 2)
        /// </summary>
        private void Timer_Tick(object? sender, EventArgs e)
        {
            _timer.Stop();
            if (button == 1)
            {
                log.Info("Left Single Click");
            }
            else if (button == 2)
            {
                log.Info("Right Single Click");

            }
        }

        /// <summary>
        /// Method to control double click for left button in mouse (if click == 2)
        /// <summary>
        private void ImageContainer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                _timer.Stop();
                log.Info("Left Double Click");
            }
            else
            {
                _timer.Start();
                button = 1;
            }
        }
        /// <summary>
        /// Method to control double click for right button in mouse (if click == 2) <
        /// <summary>
        private void ImageContainer_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                _timer.Stop();
                log.Info("Right Double Click");
                MoveToNextComic();
            }
            else
            {
                _timer.Start();
                button = 2;
            }
        }
        /// <summary>
        /// Control for image in top or buttom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageContainer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            log.Info("move screen");

            // e.VerticalOffset: represents the new updated value of the Vertical offset of the ScrollViewer
            // (after you do the scroll, means the value of the vertical offset after the event is triggered)
            if (e.VerticalChange > 0)
            {
                if (e.VerticalOffset + e.ViewportHeight == e.ExtentHeight)
                {
                    MoveToNextImage();
                }
            }
            else if (e.VerticalChange < 0)
            {
                if (e.VerticalOffset  == 0)
                {
                    MoveToPreviusImage();
                }
            }
        }

        /// <summary>
        /// Move to previus Image
        /// </summary>
        private void MoveToPreviusImage()
        {
            log.Info("Move to previus Image");
            if (Data.ImageIndex != 0)
            {
                ShowImage(Data.Images[--Data.ImageIndex]);
            }
        }

        /// <summary>
        /// Move to next Image
        /// </summary>

        private void MoveToNextImage()
        {
            log.Info("Move to next Image");
            if (Data.ImageIndex < Data.Images.Count - 1)
            {
                ShowImage(Data.Images[++Data.ImageIndex]);
            }
        }
        /// <summary>
        /// Move to previus Comic
        /// </summary>
        private void MoveToPreviusComic()
        {
            if (Data.ComicIndex > 0)
            {
                ShowImage(Data.Comics[--Data.ComicIndex]);
            }
        }

        /// <summary>
        /// Move to next Comic
        /// </summary>
        private void MoveToNextComic()
        {
            log.Info("Move to next Comic");
            if (Data.ComicIndex < Data.Comics.Count - 1)
            {
                ShowImage(Data.Comics[++Data.ComicIndex]);
            }
        }

        /// <summary>
        /// Method to show image in Image "imagePicture"
        /// </summary>
        /// <param name="imagePath">String with the route</param>
        internal void ShowImage(string imagePath)
        {
            log.Info("ShowImage: " + imagePath);
            BitmapImage image = new();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(imagePath);
            image.EndInit();
            imagePicture.Source = image;
            imageContainer.ScrollToVerticalOffset(0);
        }

        /// <summary>
        /// Method to adjunt to click when addImage in menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            log.Info("AddImage");
            OpenFile.Image();
            ShowImage(Data.Images[Data.ImageIndex]);
        }

        /// <summary>
        /// Method to adjunt to click when AddComic in menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddComic_Click(object sender, RoutedEventArgs e)
        {
            log.Info("AddComic");
            OpenFile.Comic();
            ShowImage(Data.Images[Data.ImageIndex]);
        }
    }
}
