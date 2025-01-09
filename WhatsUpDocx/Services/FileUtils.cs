using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WhatsUpDocx.Services
{
    public class FileUtils
    {
        public static IDictionary<string, List<string>> GetFiles()
        {
            string folderPath = @"C:\Users\ferd-\source\repos\sparc-coop\ibis"; // Replace with your folder path
            var fileDictionary = new Dictionary<string, List<string>>();

            if (!Directory.Exists(folderPath))
            {
                throw new InvalidOperationException($"The folder path does not exist: {folderPath}");
            }

            // Get files by each extension and add them to the dictionary
            fileDictionary.Add("razor", GetFilesByExtension(folderPath, "*.razor"));
            fileDictionary.Add("cs", GetFilesByExtension(folderPath, "*.cs"));
            fileDictionary.Add("cshtml", GetFilesByExtension(folderPath, "*.cshtml"));
            fileDictionary.Add("html", GetFilesByExtension(folderPath, "*.html"));
            fileDictionary.Add("csproj", GetFilesByExtension(folderPath, "*.csproj"));
            fileDictionary.Add("json", GetFilesByExtension(folderPath, "*.json"));
            fileDictionary.Add("css", GetFilesByExtension(folderPath, "*.css"));
            fileDictionary.Add("scss", GetFilesByExtension(folderPath, "*.scss"));
            fileDictionary.Add("js", GetFilesByExtension(folderPath, "*.js"));
            fileDictionary.Add("config", GetFilesByExtension(folderPath, "*.config"));

            return fileDictionary;
        }

        private static List<string> GetFilesByExtension(string folderPath, string extension)
        {
            return new List<string>(Directory.GetFiles(folderPath, extension, SearchOption.AllDirectories));
        }

        public static void CopyAllFilesToSpecificFolder(string destinationFolderPath)
        {
            var fileDictionary = GetFiles(); // Assuming GetFiles is your existing method

            foreach (var entry in fileDictionary)
            {
                foreach (var filePath in entry.Value)
                {
                    CopyFileToFolder(filePath, destinationFolderPath);
                }
            }
        }

        public static void CopyFileToFolder(string sourceFilePath, string destinationFolderPath)
        {
            if (!Directory.Exists(destinationFolderPath))
            {
                Directory.CreateDirectory(destinationFolderPath);
            }

            string fileName = Path.GetFileName(sourceFilePath);
            string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

            File.Copy(sourceFilePath, destinationFilePath, true);
        }

        public static void CombineRazorFilesIntoSingleFile(string destinationFilePath)
        {
            var fileDictionary = GetFiles(); // Assuming GetFiles is your existing method

            if (!fileDictionary.ContainsKey("razor"))
            {
                throw new InvalidOperationException("No Razor files found.");
            }

            StringBuilder combinedContent = new StringBuilder();

            foreach (var filePath in fileDictionary["razor"])
            {
                string content = File.ReadAllText(filePath);
                combinedContent.AppendLine("\n<!-- Start of file: " + Path.GetFileName(filePath) + " -->\n");
                combinedContent.AppendLine(content);
                // Optionally, add a separator or comment to denote different files
                combinedContent.AppendLine("\n<!-- End of file: " + Path.GetFileName(filePath) + " -->\n");
            }

            File.WriteAllText(destinationFilePath, combinedContent.ToString());
        }

        public static void CombineFilesByTypeIntoSingleFiles(string destinationFolderPath)
        {
            var fileDictionary = GetFiles(); // Assuming GetFiles is your existing method

            foreach (var fileType in fileDictionary.Keys)
            {
                StringBuilder combinedContent = new StringBuilder();

                foreach (var filePath in fileDictionary[fileType])
                {
                    string content = File.ReadAllText(filePath);
                    combinedContent.AppendLine("\n<!-- Start of file: " + Path.GetFileName(filePath) + " -->\n");
                    combinedContent.AppendLine(content);
                    // Optionally, add a separator or comment to denote different files
                    combinedContent.AppendLine("\n<!-- End of file: " + Path.GetFileName(filePath) + " -->\n");
                }

                // Construct the path for the new combined file
                string newFilePath = Path.Combine(destinationFolderPath, $"Combined{fileType}.txt");

                File.WriteAllText(newFilePath, combinedContent.ToString());
            }
        }
    }
}
