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
            if(LastImage == "Default")
            {

                ShowImage("./Resources/default.jpg");
            }

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
    }
}
