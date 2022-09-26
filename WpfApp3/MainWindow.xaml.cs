using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfApp3.src;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly System.Windows.Threading.DispatcherTimer _timer = new();
        private int button = 0;
        public MainWindow()
        {
            InitializeComponent();

            Data.Start();
            _timer.Interval = TimeSpan.FromSeconds(0.2); //wait for the other click for 200ms
            _timer.Tick += Timer_Tick;

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

        private void ImageContainer_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                _timer.Stop();
                MessageBox.Show("Right Double Click!"); //handle the double click event here...
            }
            else
            {
                _timer.Start();
                button = 2;
            }
        }

        private void ImageContainer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            if (e.VerticalChange >= 0)
            {
                if (e.VerticalOffset + e.ViewportHeight == e.ExtentHeight)
                {
                    MoveToNextImage();
                }
            }
        }

        private void MoveToNextImage()
        {
            MessageBox.Show("Move to next Image!");
        }

        internal void ShowImage(string imagePath) {
            BitmapImage image = new();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(imagePath);
            image.EndInit();
            imagePicture.Source = image;
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFile.Image();
        }

        private void AddComic_Click(object sender, RoutedEventArgs e)
        {
            OpenFile.Comic();
        }
    }
}
