using log4net;
using Microsoft.Win32;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Archives.Tar;
using SharpCompress.Common;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows;

namespace WpfApp3.src
{
    internal class UnCompress
    {
        private static string extractPath = System.IO.Path.GetTempPath() + "comicViewerExtract";

        internal static void Start(OpenFileDialog dialog)
        {
            DelExtractFiles();
            SelectZipOrRar(dialog);
        }

        internal static void DelExtractFiles()
        {
            System.IO.DirectoryInfo di = new(extractPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        internal static void SelectZipOrRar(OpenFileDialog dialog)
        {
            if (dialog.FileName.EndsWith(".cbr"))
            {
                MessageBox.Show("Open " + dialog.FileName);
                ExtractRar(dialog.FileName, extractPath);

            }
            else if (dialog.FileName.EndsWith(".cbz"))
            {
                MessageBox.Show("Open " + dialog.FileName);
                extractPath += "/" + System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                ExtractZip(dialog.FileName, extractPath);

            }
        }

        private static void ExtractRar(string path, string extractPath)
        {
            using var archive = RarArchive.Open(path);
            foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
            {
                entry.WriteToDirectory(extractPath, new ExtractionOptions()
                {

                });
            }
        }

        private static void ExtractZip(string path, string extractPath)
        {
            MessageBox.Show("Extract in " + extractPath);
            Directory.CreateDirectory(extractPath);
            ZipFile.ExtractToDirectory(path, extractPath, true);

        }
    }
}
