using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Xml;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace ComicViewer2
{

    internal class Init
    {
        private static readonly string initFile = 
            Environment.CurrentDirectory + "\\comicViewer.ini";

        public static void Start(MainWindow win)
        {
            ReadInit();
            CheckFile(win);
        }

        private static void CheckFile(MainWindow win)
        {
            try
            {
                using StreamReader sr = File.OpenText(initFile);
                string? s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    int found;
                    if (s.StartsWith("LastFolder="))
                    {
                        found = s.IndexOf("=");
                        win.LastFolder = s[(found + 1)..];
                    }
                    else if (s.StartsWith("LastFile="))
                    {
                        found = s.IndexOf("=");
                        win.LastFile = s[(found + 1)..];
                    }
                    else if (s.StartsWith("LastFormat="))
                    {
                        found = s.IndexOf("=");
                        win.LastFormat = s[(found + 1)..];
                    }
                }
            }

            catch (OutOfMemoryException e)
            {
                MessageBox.Show("fallo" + e.ToString());

            }
            catch (IOException e)
            {
                MessageBox.Show("fallo" + e.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("fallo" + e.ToString());

            }

        }

        public static void ReadInit()
        {
            // string initFile = ".\\Resources\\default.jpg";
            if (!File.Exists(initFile))
            {
                // Create a new file     
                using StreamWriter sw = File.CreateText(initFile);
                sw.WriteLine("; last modified from program " + DateTime.Now.ToString() + "\n");
                sw.WriteLine("### COMICVIEWER ###" + "\n");
                sw.WriteLine("[LastAccess]" + "\n");
                sw.WriteLine("LastFolder=" +
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\n");
                sw.WriteLine("LastComic=\n");
                sw.WriteLine("LastFormatInComic=");
                sw.WriteLine("LastImage=Default\n");
                sw.WriteLine("LastFormatInImage=" + "[\n\t0\n]\n");
            }

        }
    }


}
