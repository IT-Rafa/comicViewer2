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
    // Part of class to open files ( images or comics)
    internal static class OpenFile
    {
     // Open images files
        internal static void Image()
        {
            OpenFileDialog dialog = new()
            {
                Multiselect = true,
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp",
                InitialDirectory = Data.LastPath,
                Title = "Please select an image file."
            };

            if (dialog.ShowDialog() == true)
            {
                string t = "dialog.FileNames: \n";
                foreach (string s in dialog.FileNames)
                {
                    t += "  " + s + "\n";
                }
                MessageBox.Show(t);

                DelInfo();

                Data.Images.Clear();
                Data.Comics.Clear();
                Data.ImageIndex = 0;
                Data.ComicIndex = 0;

                Data.Images.AddRange(dialog.FileNames);
                
            }
        }
        // Open comic files
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
                string t = "dialog.filename: " + dialog.FileName + "\n";
                MessageBox.Show(t);

                DelInfo();

                UnCompress.Start(dialog);
                Data.Images.AddRange(Directory.GetFiles(extractPath));
            }
        }

        /// <summary>Del data.</summary>
        private static void DelInfo()
        {
            Data.Images.Clear();
            Data.Comics.Clear();
            Data.ImageIndex = 0;
            Data.ComicIndex = 0;
        }
    }
}
