using WebsiteTextExtractor.Core.Domain.Models;

namespace WebsiteTextExtractor.Core.Domain.Interfaces
{
    public interface IFileService
    {
        string CheckFolderExist(string path, string folderName);

        Task CheckFolders(Data data);

        void CreateDirectory(string path, string folderName);

        Task StartCreatingFiles(Data data);
    }
}