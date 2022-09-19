using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace ComicViewer2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string lastFolder = "";
        private string lastComic = "";
        private string lastImage = "";
        private string lastFormat = "";

        private readonly List<string> filePaths = new();
        private readonly List<double> vertical = new();
        private int imageIndex = 0;

        public List<string> FilePaths => filePaths;
        public List<double> Vertical => vertical;

        public string LastFolder { get => lastFolder; set => lastFolder = value; }
        public string LastComic { get => lastComic; set => lastComic = value; }
        public string LastImage { get => lastImage; set => lastImage = value; }
        public string LastFormat { get => lastFormat; set => lastFormat = value; }

        public MainWindow()
        {
            InitializeComponent();

            Init.Start(this);

        }

        private void ShowImage(string imagePath)
        {
            BitmapImage image = new();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            image.EndInit();
            imagePicture.Source = image;
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                Multiselect = true,
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp",
                InitialDirectory = lastFolder,
                Title = "Please select an image file."
            };

            if (dialog.ShowDialog() == true)
            {
                imageIndex = 0;

                filePaths.Clear();
                vertical.Clear();

                filePaths.AddRange(dialog.FileNames);

                lastFolder = dialog.FileNames[0];

                for (int x = 0; x < filePaths.Count; x++)
                {
                    vertical.Add(0);
                }

                Uri fileUri = new(filePaths[imageIndex]);
                this.Title = "Comic Viewer: Folder [" + Path.GetDirectoryName(filePaths[imageIndex]) +
                    "] [" + Path.GetFileName(filePaths[imageIndex]) + "]" + vertical[imageIndex];
                ShowImage(fileUri.ToString());
            }

        }

        private void ImagePicture_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
                e.Handled = true;

                if (e.ClickCount == 1)
                {
                    MoveToNextImage();
                }

                else if (e.ClickCount > 1)
                {
                    MoveToNextComic();
                }
            }

        private void MoveToNextImage()
        {
            vertical[imageIndex] = imageContainer.VerticalOffset;

            if (imageIndex >= 0 && imageIndex < (filePaths.Count - 1))
            {
                ++imageIndex;

                this.Title = "Comic Viewer: Comic [" + Path.GetFileName(comicActual) +
                    "] [" + Path.GetFileName(filePaths[imageIndex]) + "]";

                BitmapImage image = new();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(filePaths[imageIndex]);
                image.EndInit();
                imagePicture.Source = image;

                imageContainer.ScrollToVerticalOffset(vertical[imageIndex]);

            }
        }

        private void MoveToNextComic()
        {
            imageIndex++;
            if (filePaths[imageIndex] == null)
            {
                imageIndex = --imageIndex;
            }
            else
            {
                this.Title = "Comic Viewer: Comic [" + Path.GetFileName(comicActual) +
                        "] [" + Path.GetFileName(filePaths[imageIndex]) + "]";

                BitmapImage image = new();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(filePaths[imageIndex]);
                image.EndInit();
                imagePicture.Source = image;

                imageContainer.ScrollToVerticalOffset(vertical[imageIndex]);

            }
        }
    }

    
}
