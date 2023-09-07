using WebsiteTextExtractor.Core.Domain.Models;

namespace WebsiteTextExtractor.Core.Domain.Interfaces
{
    public interface IFileService
    {
        void BuildFiles(FileConfiguration fileConfig);
        string CheckFolderExist(string path, string folderName);
        void CreateDirectory(string path, string folderName);
        void StartCreatingFiles(FileConfiguration fileConfig, List<Chapter> chapters);
    }
}