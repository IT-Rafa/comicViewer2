using log4net;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3.src
{
    // Part of class to open files ( images or comics)
    internal static class OpenFile
    {
        private static readonly ILog log =
            LogManager.GetLogger(type: MethodBase.GetCurrentMethod()?.DeclaringType);
        // Open images files
        internal static Boolean Image()
        {
            log.Info("Ask images to user");
            OpenFileDialog dialog = new()
            {
                Multiselect = true,
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp",
                InitialDirectory = Data.LastPath,
                Title = "Please select an image file."
            };

            if (dialog.ShowDialog() == true)
            {
                log.Info("read comic: " + dialog.FileName);

                DelInfo();

                Data.Images.Clear();
                Data.Comics.Clear();
                Data.ImageIndex = 0;
                Data.ComicIndex = 0;

                Data.Images.AddRange(dialog.FileNames);
                return true;
            }
            return false;
        }
        // Open comic files
        internal static Boolean Comic()
        {
            log.Info("Ask comic to user");

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
                log.Info("read comic: " +  dialog.FileName);

                DelInfo();

                UnCompress.Start(dialog);
                Data.Images.AddRange(Directory.GetFiles(extractPath));
                return true;
            }
            return false;
        }

        /// <summary>Del data.</summary>
        private static void DelInfo()
        {
            log.Info("deleting images");
            Data.Images.Clear();
            Data.Comics.Clear();
            Data.ImageIndex = 0;
            Data.ComicIndex = 0;
        }
    }
}
