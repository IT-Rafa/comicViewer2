using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ComicViewer2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            Init.Start(this);

        }

        private void ShowImage(string imagePath, string source, string fileName)
        {
            this.Title = "Comic Viewer: Comic [" + source + "] [" + fileName + "]";

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
                InitialDirectory = Init.LastPath,
                Title = "Please select an image file."
            };

            if (dialog.ShowDialog() == true)
            {
                OpenFile.DelExtractPath(this, dialog);

            }

        }
        private void AddComicFile_Click(object sender, RoutedEventArgs e)
        {
            string extractPath = System.IO.Path.GetTempPath() + "comicViewerExtract";
            Directory.CreateDirectory(extractPath);

            OpenFileDialog dialog = new()
            {
                Filter = "Comic Files(*.cbr; *.cbz;)|*.cbr; *.cbz",
                InitialDirectory = Init.LastPath,
                Title = "Please select an comic file."
            };

            OpenFile.DelExtractPath(this, dialog);
        }
        private void ImagePicture_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (e.ClickCount == 1)
            {
                MoveToPreviousImage();
            }

            else if (e.ClickCount > 1)
            {
                MoveToPreviousComic();
            }
        }



        private void MoveToPreviousImage()
        {

            --Init.ImageIndex;
            if (Init.ImageIndex < 0)
            {
                ++Init.ImageIndex;
            }

            ShowImage(Init.Images[Init.ImageIndex],"","");
            //imageContainer.ScrollToVerticalOffset(Init.Vertical[Init.ImageIndex]);
        }

        private void MoveToNextImage()
        {
            ++Init.ImageIndex;
            if (Init.ImageIndex >= Init.Images.Count)
            {
                --Init.ImageIndex;
            }

            ShowImage(Init.Images[Init.ImageIndex], "", "");
            // imageContainer.ScrollToVerticalOffset(Init.Vertical[Init.ImageIndex]);
        }
        private void MoveToPreviousComic()
        {
            Init.ImageIndex--;
            if (Init.ImageIndex <= 0)
            {
                ++Init.ImageIndex;
            }

            ShowImage(Init.Images[Init.ImageIndex], "", "");
            //imageContainer.ScrollToVerticalOffset(Init.Vertical[Init.ImageIndex]);

        }
        private void MoveToNextComic()
        {
            Init.ImageIndex++;
            if (Init.ImageIndex >= Init.Images.Count)
            {
                --Init.ImageIndex;
            }

            ShowImage(Init.Images[Init.ImageIndex],"","");
           // imageContainer.ScrollToVerticalOffset(Init.Vertical[Init.ImageIndex]);
        }

        private void ImagePicture_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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


    }


}
