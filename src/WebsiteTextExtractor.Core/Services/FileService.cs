using System.Text;
using WebsiteTextExtractor.Core.Domain.Interfaces;
using WebsiteTextExtractor.Core.Domain.Models;

namespace WebsiteTextExtractor.Core.Services
{
    /// <summary>Represents the FileService to Create/Read/Write files.</summary>
    public class FileService : IFileService
    {
        private const string FOLDER_BOOKS_GENERATED = "BooksGenerated";
        private readonly IDirectoryProvider _directoryProvider;

        public FileService(IDirectoryProvider directoryProvider)
        {
            _directoryProvider = directoryProvider;
        }

        public Task CheckFolders(Data data)
        {
            // TODO: Refactor this method.
            data.FileConfiguration.Path = CheckFolderExist(data.FileConfiguration.Path, FOLDER_BOOKS_GENERATED);
            data.FileConfiguration.Path = CheckFolderExist(data.FileConfiguration.Path, data.FileConfiguration.FileName);
            data.FileConfiguration.Path = CheckFolderExist(data.FileConfiguration.Path, data.FileConfiguration.Language);
            return Task.CompletedTask;
        }

        /// <summary>Execute the creation chapter files.</summary>
        public Task StartCreatingFiles(Data data)
        {
            RunCreateBigBook(data);
            Console.WriteLine(
                $"Created the book: {data.FileConfiguration.FileName}" +
                $" - Chapter: {data.FileConfiguration.StartChapter}" +
                $" to {data.FileConfiguration.EndChapter}.");

            RunCreateBigBook(data);
            return Task.CompletedTask;
        }

        /// <summary>Execute the creation chapter files.</summary>
        private void RunCreateBigBook(Data data)
        {
            try
            {
                string fileName = $"Chapter {data.FileConfiguration.StartChapter} to {data.FileConfiguration.EndChapter}.{data.FileConfiguration.Type}";
                string filePath = $"{data.FileConfiguration.Path}{fileName}";
                string inputText = string.Empty;
                foreach (Chapter chapter in data.Chapters)
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
        public void RunFileCreation(FileConfiguration fileConfig, string inputText, uint chapter)
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

        private void BuildFiles(FileConfiguration fileConfig)
        {
            var path = $"{fileConfig.Path}{fileConfig.Language}/{fileConfig.FileName}_{fileConfig.Chapter}_{fileConfig.Language}.{fileConfig.Type}";
            HasFile(path);
        }

        public void CreateDirectory(string path, string folderName)
        {
            path = CheckSlashPath(path);
            path = UpdateFolderName(path, folderName);
            if (!_directoryProvider.HasDirectory(path)) _directoryProvider.CreateDirectory(path);
        }

        public string CheckFolderExist(string path, string folderName)
        {
            try
            {
                path = CheckSlashPath(path);
                path = UpdateFolderName(path, folderName);
                if (!_directoryProvider.HasDirectory(path)) _directoryProvider.CreateDirectory(path);
                return path;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
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

        private static string CheckSlashPath(string path) =>
            !path.EndsWith('\\') ? $"{path}\\" : path;

        private static string UpdateFolderName(string path, string folderName)
        {
            path += folderName;
            return !path.EndsWith('\\') ? $"{path}\\" : path;
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