using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3.src
{
    internal static class Data
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
                        LastPath = s.Substring("lastPath=".Length);
                    }
                    else if (s.StartsWith("images=["))
                    {
                        while((s = sr.ReadLine()) != null)
                        {
                            if (s == "]")
                                break;
                            else
                            {
                                Images.Add(s.Substring("\t".Length));
                            }
                        }
                    }
                    else if (s.StartsWith("imageIndex="))
                    {
                        ImageIndex = int.Parse( s.Substring("imageIndex=".Length));
                    }
                    else if (s.StartsWith("comics=["))
                    {
                        while ((s = sr.ReadLine()) != null)
                        {
                            if (s == "]")
                                break;
                            else
                            {
                                Comics.Add(s.Substring("\t".Length));
                            }
                        }
                    }
                    else if (s.StartsWith("comicIndex="))
                    {
                        ComicIndex = int.Parse(s.Substring("comicIndex=".Length));

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

        
    }
}
