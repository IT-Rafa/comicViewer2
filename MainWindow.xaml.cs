using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ComicViewer2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<string> filePaths = new();
        private readonly List<double> vertical = new();
        private int imageIndex = 0;

        public List<string> FilePaths => filePaths;
        public List<double> Vertical => vertical;

        public MainWindow()
        {
            InitializeComponent();
            Init.Start();

        }



        private void ShowImage(string imagePath)
        {

            BitmapImage image = new();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri("imagen1.jpg");
            image.EndInit();
            imagePicture.Source = image;
        }
    }
}
