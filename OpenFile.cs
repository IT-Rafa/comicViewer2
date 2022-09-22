using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComicViewer2
{
    internal class OpenFile
    {
        public static void  DelExtractPath(MainWindow win, OpenFileDialog dialog)
        {
            Init.Images.Clear();
            foreach (string s in dialog.FileNames)
            {
                Init.Images.Add(s);
            }
           
        }


    }
}
