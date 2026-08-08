using System;
using System.IO;

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
            string[] folders = ["Documents", "Images", "Music", "Videos", "Compressed"];
            int totalFilesMoved = 0;

            // File format arrays
            string[] fileFormatsDocuments = [".txt", ".pdf", ".docx", ".psd", ".kra", ".aep", ".csp"];
            string[] fileFormatsImages = [".png", ".jpg", ".jpeg", ".webp", ".gif", ".svg"];
            string[] fileFormatsMusic = [".mp3", ".wav", ".aac"];
            string[] fileFormatsVideos = [".mp4", ".mov", ".mkv", ".avi", ".webm"];
            string[] fileFormatsCompressed = [".zip", ".rar", ".7z", ".tar", ".tar.gz"];

            for (int i = 0; i < folders.Length; i++)
            {
                string currentFolder = $"{userDefinedPath}/{folders[i]}";
                Console.WriteLine($"Creating {currentFolder}...");
                Directory.CreateDirectory(currentFolder);
            }

            // Documents
            for (int i = 0; i < fileFormatsDocuments.Length; i++)
            {
                totalFilesMoved += MoveFilesOfType(userDefinedPath, fileFormatsDocuments[i], $"{userDefinedPath}/Documents");
            }

            // Images
            for (int i = 0; i < fileFormatsImages.Length; i++)
            {
                totalFilesMoved += MoveFilesOfType(userDefinedPath, fileFormatsImages[i], $"{userDefinedPath}/Images");
            }

            // Music
            for (int i = 0; i < fileFormatsMusic.Length; i++)
            {
                totalFilesMoved += MoveFilesOfType(userDefinedPath, fileFormatsMusic[i], $"{userDefinedPath}/Music");
            }

            // Videos
            for (int i = 0; i < fileFormatsVideos.Length; i++)
            {
                totalFilesMoved += MoveFilesOfType(userDefinedPath, fileFormatsVideos[i], $"{userDefinedPath}/Videos");
            }

            // Compressed
            for (int i = 0; i < fileFormatsCompressed.Length; i++)
            {
                totalFilesMoved += MoveFilesOfType(userDefinedPath, fileFormatsCompressed[i], $"{userDefinedPath}/Compressed");
            }

            Console.WriteLine($"Total files moved: {totalFilesMoved}");
        }
        public static int MoveFilesOfType(string sourcePath, string extension, string targetFolder)
        {
            var files = Directory.EnumerateFiles(sourcePath, $"*{extension}");
            int countFiles = 0;
            foreach (string file in files)
            {
                string fileName = file.Substring(sourcePath.Length + 1);
                string combinedPath = Path.Combine(targetFolder, fileName);
                if (File.Exists($"{combinedPath}"))
                {
                    Console.WriteLine($"⚠ WARNING!: skipping {fileName} as it already exists in {targetFolder}.");
                }
                else
                {
                    Console.WriteLine($"Moving {fileName} to {targetFolder}...");
                    File.Move(file, combinedPath);
                    countFiles++;
                }
            }
            return countFiles;
        }
    }
}