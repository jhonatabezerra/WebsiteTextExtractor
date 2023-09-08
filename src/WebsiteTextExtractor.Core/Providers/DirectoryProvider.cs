using WebsiteTextExtractor.Core.Domain.Interfaces;

namespace WebsiteTextExtractor.Core.Providers
{
    public class DirectoryProvider : IDirectoryProvider
    {
        public bool HasDirectory(string path) => Directory.Exists(path);

        public void CreateDirectory(string path) => Directory.CreateDirectory(path);
    }
}