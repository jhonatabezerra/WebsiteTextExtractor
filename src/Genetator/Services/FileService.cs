using ConsoleApp1.Domain;
using System.Text;

namespace ConsoleApp1.Services
{
    /// <summary>Represents the FileService to Create/Read/Write files.</summary>
    public class FileService
    {
        /// <summary>Execute the creation chapter files.</summary>
        /// <param name="fileConfig">The <see cref="FileConfiguration"/> instance.</param>
        /// <param name="inputText">The chapter text.</param>
        public void RunFileCreation(FileConfiguration fileConfig, string inputText)
        {
            string filePath = $"{fileConfig.Path}{fileConfig.Language}/{fileConfig.FileName}_{fileConfig.Chapter}_{fileConfig.Language}.{fileConfig.Type}";
            try
            {
                CheckFileExist(filePath);
                CreateFile(filePath, inputText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public async Task BuildFiles(FileConfiguration fileConfig)
        {
            var path = $"{fileConfig.Path}{fileConfig.Language}/{fileConfig.FileName}_{fileConfig.Chapter}_{fileConfig.Language}.{fileConfig.Type}";
            CheckFileExist(path);
        }

        #region Private Methods

        /// <summary>Check if file already exists. If yes, delete it.</summary>
        /// <param name="filePath">The file path.</param>
        private void CheckFileExist(string filePath)
        {
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        /// <summary>Create a new file and Add some text to file.</summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="inputText">The text that will be inserted into the file.</param>
        private void CreateFile(string filePath, string inputText)
        {
            using FileStream fs = File.Create(filePath);
            Byte[] title = new UTF8Encoding(true).GetBytes(inputText);
            fs.Write(title, 0, title.Length);
        }

        /// <summary>Open the stream and read it back.</summary>
        /// <param name="filePath">The file path.</param>
        private string ReadFile(string filePath)
        {
            using StreamReader streamReader = File.OpenText(filePath);
            return streamReader.ReadToEnd();
        }

        #endregion Private Methods
    }
}