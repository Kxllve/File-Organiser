using System;
using System.IO;

namespace FileOrganiser
{
    class Program
    {
        public static void Main(string[] args)
        {
            var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
             string defaultPath = Path.Combine(home, "Downloads");
                Console.WriteLine($"Please specify the folder to scan (default: {defaultPath}): ");
                var userDefinedPath = Console.ReadLine();

            if (String.IsNullOrEmpty(userDefinedPath))
            {
                userDefinedPath = defaultPath;
            }

            if (!Directory.Exists(userDefinedPath))
            {
                Console.WriteLine($"The path '{userDefinedPath}' does not exist or is not a valid directory.");
                return;
            }

            try
            {
                Directory.EnumerateFileSystemEntries(userDefinedPath).FirstOrDefault();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"You don't have permission to read from '{userDefinedPath}'.");
                return;
            }

            Console.WriteLine($"{userDefinedPath} will be used.");
            SortFiles(userDefinedPath);
        }
        public static void SortFiles(string userDefinedPath)
        {
            string[] folders = ["Documents", "Images", "Music", "Videos", "Compressed"];
            int totalFilesMoved = 0;

            Dictionary<string, List<string>> fileFormats = new();
            fileFormats.Add("Documents", new List<string> { ".txt", ".pdf", ".psd", ".docx", ".doc", ".xlsx", ".xls", ".pptx", ".ppt", ".rtf", ".odt", ".csv", ".json", ".xml", ".html", ".htm", ".md" });
            fileFormats.Add("Images", new List<string> { ".png", ".jpg", ".jpeg", ".webp", ".gif", ".svg", ".bmp", ".tif", ".tiff", ".ico", ".heic", ".heif", ".raw", ".cr2", ".nef", ".ai", ".eps" });
            fileFormats.Add("Music", new List<string> { ".mp3", ".wav", ".aac", ".flac", ".ogg", ".oga", ".m4a", ".alac", ".wma", ".mid", ".midi" });
            fileFormats.Add("Videos", new List<string> { ".mp4", ".mov", ".mkv", ".avi", ".wmv", ".flv", ".m4v", ".3gp", ".webm" });
            fileFormats.Add("Compressed", new List<string> { ".zip", ".rar", ".7z", ".tar", ".tbz2", ".gz", ".iso", ".img", ".dmg", ".tar.gz", ".tar.bz2", ".tar.xz" });
            string[] compoundExtensions = { ".tar.gz", ".tar.bz2", ".tar.xz" };

            foreach (var folderName in folders)
            {
                string currentFolder = Path.Combine(userDefinedPath, folderName);
                Console.WriteLine($"Creating {currentFolder}...");
                Directory.CreateDirectory(currentFolder);
            }

            foreach (var (folderName, extensions) in fileFormats)
            {
                foreach (var extension in extensions)
                {
                    totalFilesMoved += MoveFilesOfType(userDefinedPath, extension, Path.Combine(userDefinedPath, folderName), compoundExtensions);
                }
            }

            Console.WriteLine($"Total files moved: {totalFilesMoved}");
        }
        public static int MoveFilesOfType(string sourcePath, string extension, string targetFolder, string[] compoundExtensions)
        {
            var files = Directory.EnumerateFiles(sourcePath);
            int countFiles = 0;
            foreach (string file in files)
            {
                string ext = Path.GetExtension(file);
                foreach (var compoundSuffix in compoundExtensions)
                {
                    if (compoundSuffix.StartsWith(extension, StringComparison.OrdinalIgnoreCase))
                    {
                        ext = compoundSuffix;
                        break;
                    }
                }
                if (string.Equals(ext, extension, StringComparison.OrdinalIgnoreCase))
                {
                    string fileName = Path.GetFileName(file);
                    string combinedPath = Path.Combine(targetFolder, fileName);
                    if (File.Exists(combinedPath))
                    {
                        Console.WriteLine($"⚠ WARNING!: skipping {fileName} as it already exists in {targetFolder}.");
                    }
                    else
                    {
                        try
                        {
                            File.Move(file, combinedPath);
                            Console.WriteLine($"Moving {fileName} to {targetFolder}...");
                            countFiles++;
                        }
                        catch (UnauthorizedAccessException)
                        {
                            Console.WriteLine($"Couldn't move {fileName}, you don't have permission to do that.");
                        }
                    }
                }
            }
            return countFiles;
        }
    }
}