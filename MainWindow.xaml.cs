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

        public MainWindow()
        {
            InitializeComponent();
            UpdateInit();

        }
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        private void UpdateInit()
        {
            // Comprobar archivos
            //      - xml con datos de acceso
            //      - archivo temporal - vaciar
            //string curFile = "\test.txt";
            //Console.WriteLine(File.Exists(curFile) ? "File exists." : "File does not exist.");
        }
    }
}
