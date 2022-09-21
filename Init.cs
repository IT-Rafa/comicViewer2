using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using File = System.IO.File;

namespace ComicViewer2
{

    internal class Init
    {
        private static string lastPath = "";
        private static readonly List<string> images = new();
        private static int imageIndex = 0;

        private static readonly string initFile = 
            Environment.CurrentDirectory + "\\comicViewer.ini";

        public static string LastPath { get => lastPath; set => lastPath = value; }

        public static List<string> Images => images;

        public static int ImageIndex { get => imageIndex; set => imageIndex = value; }

        public static void Start(MainWindow win)
        {
            ReadInit();
            CheckFile();
        }

        private static void CheckFile()
        {
            try
            {
                using StreamReader sr = File.OpenText(initFile);
                string? s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    int found;
                    if (s.StartsWith("lastPath="))
                    {
                        found = s.IndexOf("=");
                        LastPath = s[(found + 1)..];
                    }
                    else if (s.StartsWith("images="))
                    {
                        found = s.IndexOf("=");
                        Images.Add(s[(found + 1)..]);
                    }
                    else if (s.StartsWith("LastFile="))
                    {
                        found = s.IndexOf("=");
                        ImageIndex = int.Parse(s[(found + 1)..]);
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
                sw.WriteLine("lastPath=" +
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\n");
                sw.WriteLine("images=\n");
                sw.WriteLine("imageIndex=");

            }

        }
    }


}
