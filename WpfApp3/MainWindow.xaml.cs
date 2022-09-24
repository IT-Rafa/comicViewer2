using System.Windows;
using System.Windows.Input;
using WpfApp3.src;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Data.Start();
        }

        private void ImageContainer_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                MessageBox.Show("Left DoubleClick");
            }
            else if(e.ChangedButton == MouseButton.Right)
            {
                MessageBox.Show("Right DoubleClick");

            }

        }

        private void ImageContainer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                MessageBox.Show("Left Click");
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                MessageBox.Show("Right Click");

            }
        }
    }
}
