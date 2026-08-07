using System;
using System.IO;
using System.Reflection.Metadata;

namespace FileOrganiser
{
    class Program
    {
        public static void Main(string[] args)
        {
            var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string defaultPath = $"{home}/Downloads";

            Console.WriteLine($"Please specify the folder to scan (default: {defaultPath}: ");
            string userDefinedPath = Console.ReadLine();

            if (String.IsNullOrEmpty(userDefinedPath))
            {
                userDefinedPath = defaultPath;
            }

            Console.WriteLine($"{userDefinedPath} will be used.");

            SortFiles(userDefinedPath);

        }
        public static void SortFiles(string userDefinedPath)
        {
            string[] folders = { "Documents", "Images", "Music", "Videos", "Compressed" };
            for (int i = 0; i < folders.Length; i++)
            {
                string currentFolder = $"{userDefinedPath}/{folders[i]}";
                Console.WriteLine($"Creating {currentFolder}...");
                Directory.CreateDirectory(currentFolder);
            }
            // Documents
            MoveFilesOfType(userDefinedPath, ".txt", $"{userDefinedPath}/Documents");
            MoveFilesOfType(userDefinedPath, ".pdf", $"{userDefinedPath}/Documents");
            MoveFilesOfType(userDefinedPath, ".docx", $"{userDefinedPath}/Documents");
            MoveFilesOfType(userDefinedPath, ".psd", $"{userDefinedPath}/Documents");
            MoveFilesOfType(userDefinedPath, ".kra", $"{userDefinedPath}/Documents");
            MoveFilesOfType(userDefinedPath, ".aep", $"{userDefinedPath}/Documents");
            MoveFilesOfType(userDefinedPath, ".csp", $"{userDefinedPath}/Documents");
            // Images
            MoveFilesOfType(userDefinedPath, ".png", $"{userDefinedPath}/Images");
            MoveFilesOfType(userDefinedPath, ".jpg", $"{userDefinedPath}/Images");
            MoveFilesOfType(userDefinedPath, ".jpeg", $"{userDefinedPath}/Images");
            MoveFilesOfType(userDefinedPath, ".webp", $"{userDefinedPath}/Images");
            MoveFilesOfType(userDefinedPath, ".gif", $"{userDefinedPath}/Images");
            MoveFilesOfType(userDefinedPath, ".svg", $"{userDefinedPath}/Images");
            // Music
            MoveFilesOfType(userDefinedPath, ".mp3", $"{userDefinedPath}/Music");
            MoveFilesOfType(userDefinedPath, ".wav", $"{userDefinedPath}/Music");
            MoveFilesOfType(userDefinedPath, ".aac", $"{userDefinedPath}/Music");
            // Videos
            MoveFilesOfType(userDefinedPath, ".mp4", $"{userDefinedPath}/Videos");
            MoveFilesOfType(userDefinedPath, ".mov", $"{userDefinedPath}/Videos");
            MoveFilesOfType(userDefinedPath, ".mkv", $"{userDefinedPath}/Videos");
            MoveFilesOfType(userDefinedPath, ".avi", $"{userDefinedPath}/Videos");
            MoveFilesOfType(userDefinedPath, ".webm", $"{userDefinedPath}/Videos");
            // Compressed
            MoveFilesOfType(userDefinedPath, ".zip", $"{userDefinedPath}/Compressed");
            MoveFilesOfType(userDefinedPath, ".rar", $"{userDefinedPath}/Compressed");
            MoveFilesOfType(userDefinedPath, ".7z", $"{userDefinedPath}/Compressed");
            MoveFilesOfType(userDefinedPath, ".tar", $"{userDefinedPath}/Compressed");
            MoveFilesOfType(userDefinedPath, ".tar.gz", $"{userDefinedPath}/Compressed");
        }
        public static void MoveFilesOfType(string sourcePath, string extension, string targetFolder)
        {
            var files = Directory.EnumerateFiles(sourcePath, $"*{extension}");
            foreach (string file in files)
            {
                string fileName = file.Substring(sourcePath.Length + 1);
                File.Move(file, Path.Combine(targetFolder, fileName));
            }
        }
    }
}