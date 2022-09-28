using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfApp3.src;

namespace WpfApp3
{
    /// <summary>
    /// Create and control user app
    /// </summary>
    public partial class MainWindow : Window
    {
        /** temp for prepare click or double click in mouse button
         */
        private readonly System.Windows.Threading.DispatcherTimer _timer = new();
        
        /** button pressed in mouse
         */
        private int button = 0;

        /// <summary>
        /// Constructor. Prepare board of app and controls of user interactions
        /// </summary>
        public MainWindow()
        {
            // prepare page in xaml
            InitializeComponent();

            // get user data and prepare the image control
            Data.Start();

            // start tempore for mouse
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
                MessageBox.Show("Left Single Click!"); //handle the single click event here...
            }
            else if (button == 2)
            {
                MessageBox.Show("Right Single Click!"); //handle the single click event here...
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
                MessageBox.Show("Left Double Click!"); //handle the double click event here...

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
                MessageBox.Show("Right Double Click!"); //handle the double click event here...
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
            MessageBox.Show("Move to previus Image!");
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
            MessageBox.Show("Move to next Image!");
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
            MessageBox.Show("Move to previus Comic!");
            if (Data.ComicIndex != 0)
            {
                ShowImage(Data.Comics[--Data.ComicIndex]);
            }
        }

        /// <summary>
        /// Move to next Comic
        /// </summary>
        private void MoveToNextComic()
        {
            MessageBox.Show("Move to next Comic!");
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
            BitmapImage image = new();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(imagePath);
            image.EndInit();
            imagePicture.Source = image;
        }

        /// <summary>
        /// Method to adjunt to click when addImage in menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
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
            OpenFile.Comic();
        }
    }
}
