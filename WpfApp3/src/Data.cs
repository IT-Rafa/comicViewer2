using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3.src
{
    internal class Data
    {
        private static string lastPath = "";

        private static readonly List<string> images = new();
        private static int imageIndex = 0;

        private static readonly List<string> comics = new();
        private static int comicIndex = 0;

        private static readonly string initFile =
            Environment.CurrentDirectory + "\\comicViewer.ini";

        //
        public static string LastPath { get => lastPath; set => lastPath = value; }

        public static List<string> Images => images;

        public static int ImageIndex { get => imageIndex; set => imageIndex = value; }

        public static List<string> Comics => comics;

        public static int ComicIndex { get => comicIndex; set => comicIndex = value; }
        //
        public static void Start()
        {
            ReadInit();
            CheckFile();
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

                sw.WriteLine("images=[\n\tdefault\n]\n");
                sw.WriteLine("imageIndex=2\n");

                sw.WriteLine("comics=[\n\tdefault\n]\n");
                sw.WriteLine("comicIndex=3\n");

            }

        }
        private static void CheckFile()
        {
            try
            {
                using StreamReader sr = File.OpenText(initFile);
                string? s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    ;
                    if (s.StartsWith("lastPath="))
                    {
                        int found = s.IndexOf("=");
                        LastPath = s[(found + 1)..];
                    }
                    else if (s.StartsWith("images=["))
                    {
                        while((s = sr.ReadLine()) != null)
                        {
                            if (s == "]")
                                break;
                            else
                            {
                                Images.Add(s);
                            }
                        }
                    }
                    else if (s.StartsWith("imageIndex="))
                    {

                    }
                    else if (s.StartsWith("comics=["))
                    {
                        while ((s = sr.ReadLine()) != null)
                        {
                            if (s == "]")
                                break;
                            else
                            {
                                Comics.Add(s);
                            }
                        }
                    }
                    else if (s.StartsWith("comicIndex="))
                    {
                        LastPath = s.Substring(s.IndexOf("=") + 2);
                    }

                }
                string msg = "";
                msg += "LastPath=" + LastPath + "\n";

                msg += "images=[";
                foreach (var image in Images)
                {
                    msg += image;
                }
                msg += "]\n";
                msg += "imageIndex=" + imageIndex + "\n";

                msg += "comic=[";
                foreach (var comic in Comics)
                {
                    msg += comic;
                }
                msg += "]\n";
                msg += "comicIndex=" + comicIndex + "\n";

                MessageBox.Show(msg);

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

        
    }
}
