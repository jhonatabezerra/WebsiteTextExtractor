using WebsiteTextExtractor.Core.Domain;
using WebsiteTextExtractor.Core.Services;
using Xunit;

namespace WebsiteTextExtractor.Core.Tests.Services
{
    public class FileServiceTests
    {
        private readonly FileService _fileService;

        public FileServiceTests() => _fileService = new();

        [Fact]
        public void RunFileCreation_WhenCall_ShouldCreateFile()
        {
            // Arrange
            const uint CHAPTER = 1;
            const string TEXT = "Fui criado com sucesso";
            FileConfiguration FileConfiguration = new();

            // Act
            FileService.RunFileCreation(FileConfiguration, TEXT, CHAPTER);
        }
    }
}