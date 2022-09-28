using Microsoft.Win32;
using System.IO;
using System.IO.Compression;
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
                extractPath += System.IO.Path.GetFileNameWithoutExtension(dialog.FileName);
                ExtractZip(dialog.FileName, extractPath);

            }
        }

        private static void ExtractRar(string path, string extractPath)
        {
            MessageBox.Show("Extract in " + extractPath);
            ZipFile.ExtractToDirectory(path, extractPath, true);
        }

        private static void ExtractZip(string path, string extractPath)
        {
            MessageBox.Show("Extract in " + extractPath);
        }
    }
}
