namespace WebsiteTextExtractor.Core.Domain.Interfaces
{
    public interface IDirectoryProvider
    {
        void CreateDirectory(string path);
        bool HasDirectory(string path);
    }
}