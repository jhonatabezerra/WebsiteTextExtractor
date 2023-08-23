using System.Text;
using WebsiteTextExtractor.Core.Domain;

namespace WebsiteTextExtractor.Core.Services
{
    /// <summary>Represents the FileService to Create/Read/Write files.</summary>
    public class FileService
    {
        /// <summary>Execute the creation chapter files.</summary>
        /// <param name="fileConfig">The <see cref="FileConfiguration"/> instance.</param>
        /// <param name="inputText">The chapter text.</param>
        public static void RunCreateBigBook(FileConfiguration fileConfig, List<Chapter> chapters)
        {
            try
            {
                string fileName = $"Chapter {fileConfig.StartChapter} to {fileConfig.EndChapter}.{fileConfig.Type}";
                string filePath = $"{fileConfig.Path}{fileName}";
                string inputText = string.Empty;
                foreach (Chapter chapter in chapters)
                {
                    inputText += $"{chapter.ChapterTitle} \n\n";
                    inputText += $"{chapter.ChapterText} \n\n";
                }

                if (!HasFile(filePath)) File.Delete(filePath);
                CreateFile(filePath, fileName, inputText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>Execute the creation chapter files.</summary>
        /// <param name="fileConfig">The <see cref="FileConfiguration"/> instance.</param>
        /// <param name="inputText">The chapter text.</param>
        public static void RunFileCreation(FileConfiguration fileConfig, string inputText, uint chapter)
        {
            try
            {
                string filePath = string.Empty;

                if (string.IsNullOrEmpty(fileConfig.Title))
                    filePath = $"{fileConfig.Path}{fileConfig.FileName}_{chapter}.{fileConfig.Type}";
                else
                    filePath = $"{fileConfig.Path}{GetTitle(fileConfig.Title)}.{fileConfig.Type}";

                if (HasFile(filePath)) File.Delete(filePath);
                CreateFile(filePath, fileConfig.Title, inputText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static void BuildFiles(FileConfiguration fileConfig)
        {
            var path = $"{fileConfig.Path}{fileConfig.Language}/{fileConfig.FileName}_{fileConfig.Chapter}_{fileConfig.Language}.{fileConfig.Type}";
            HasFile(path);
        }

        public static string CheckFolderExist(string path, string folderName)
        {
            path = CheckPath(path);
            path = UpdateFolder(path, folderName);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        #region Private Methods

        /// <summary>Check if file already exists. If yes, delete it.</summary>
        /// <param name="filePath">The file path.</param>
        private static bool HasFile(string filePath) => File.Exists(filePath);

        /// <summary>Create a new file and Add some text to file.</summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="inputText">The text that will be inserted into the file.</param>
        private static void CreateFile(string filePath, string? inputTitle, string inputText)
        {
            using FileStream fileStream = File.Create(filePath);

            WriteInFile(fileStream, inputTitle);
            WriteInFile(fileStream, inputText);
        }

        /// <summary>Create a new file and Add some text to file.</summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="inputText">The text that will be inserted into the file.</param>
        private static void AddTextInFile(string filePath, string? inputTitle, string inputText)
        {
            using FileStream fileStream = File.OpenWrite(filePath);

            WriteInFile(fileStream, inputTitle);
            WriteInFile(fileStream, inputText);
        }

        private static void WriteInFile(FileStream fileStream, string inputText)
        {
            byte[] title = new UTF8Encoding(true).GetBytes(inputText + "\n\n");

            if (!string.IsNullOrEmpty(inputText)) Write(fileStream, title, title.Length);
        }

        private static void Write(FileStream fileStream, byte[] textInByteArray, int textLength)
        {
            fileStream.Write(textInByteArray, 0, textLength);
            //fileStream.Close();
        }

        /// <summary>Open the stream and read it back.</summary>
        /// <param name="filePath">The file path.</param>
        private static string ReadFile(string filePath)
        {
            using StreamReader streamReader = File.OpenText(filePath);
            return streamReader.ReadToEnd();
        }

        private static string CheckPath(string path)
        {
            if (!path.EndsWith('\\')) return $"{path}\\";
            return path;
        }

        private static string UpdateFolder(string path, string newFolder)
        {
            path += newFolder;
            if (!path.EndsWith('\\')) return $"{path}\\";
            return path;
        }

        private static string GetTitle(string title)
        {
            var jogo = '#';
            if (title.Contains('*')) title = title.Replace('*', jogo);
            if (title.Contains('|')) title = title.Replace('|', jogo);
            if (title.Contains('/')) title = title.Replace('/', jogo);
            if (title.Contains('\\')) title = title.Replace('\\', jogo);
            if (title.Contains('?')) title = title.Replace('?', jogo);
            if (title.Contains(':')) title = title.Replace(':', jogo);
            if (title.Contains(';')) title = title.Replace(';', jogo);
            if (title.Contains('\"')) title = title.Replace('\"', jogo);
            if (title.Contains('\\')) title = title.Replace('\\', jogo);
            if (title.Contains('<')) title = title.Replace('<', jogo);
            if (title.Contains('>')) title = title.Replace('>', jogo);

            return title;
        }

        #endregion Private Methods
    }
}