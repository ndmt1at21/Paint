using System;
using System.Collections.Generic;
using System.IO;

namespace Paint.Utils
{
    public static class File
    {
        private static Dictionary<string, int> filePaths = new Dictionary<string, int>();

        public static void RenameFile(string path, string newFileName)
        {
            var dir = Path.GetDirectoryName(path);

            if (dir == null)
                throw new Exception("Folder not found");

            MoveFile(path, dir, newFileName);
        }

        public static void MoveFile(string from, string toDir, string newFileName)
        {
            if (!System.IO.File.Exists(from))
            {
                throw new FileNotFoundException("File not found");
            }
        }

        public static string? GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public static string? GetDirectoryName(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        public static void MakeDirectoryHidden(string path)
        {
            if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                if ((directoryInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    directoryInfo.Attributes |= FileAttributes.Hidden;
                }
            }
        }

        public static void MakeFileHidden(string filename)
        {
            if (!string.IsNullOrWhiteSpace(filename) && System.IO.File.Exists(filename))
            {
                FileInfo fileInfo = new FileInfo(filename);
                if ((fileInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    fileInfo.Attributes |= FileAttributes.Hidden;
                }
            }
        }
    }
}