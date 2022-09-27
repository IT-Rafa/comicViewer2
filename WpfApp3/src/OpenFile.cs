using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3.src
{
    internal static class OpenFile
    {
        internal static void Image()
        {
            OpenFileDialog dialog = new()
            {
                Multiselect = true,
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp",
                InitialDirectory = Data.LastPath,
                Title = "Please select an image file."
            };
            MessageBox.Show("InitialDirectory" + dialog.InitialDirectory);

            if (dialog.ShowDialog() == true)
            {
                
            }
        }

        internal static void Comic()
        {
            string extractPath = System.IO.Path.GetTempPath() + "comicViewerExtract";
            Directory.CreateDirectory(extractPath);

            OpenFileDialog dialog = new()
            {
                Filter = "Comic Files(*.cbr; *.cbz;)|*.cbr; *.cbz",
                InitialDirectory = Data.LastPath,
                Title = "Please select an comic file."
            };

            if (dialog.ShowDialog() == true)
            {

            }
        }
    }
}
