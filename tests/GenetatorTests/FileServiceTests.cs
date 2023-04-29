using ConsoleApp1.Domain;
using ConsoleApp1.Services;

namespace TestProject1
{
    public class FileServiceTests
    {
        private readonly FileService _fileService;

        public FileServiceTests()
        {
            _fileService = new();
        }

        [Fact]
        public void Test()
        {
            // Arrange
            var txt = "Fui criado com sucesso";

            // Act
            _fileService.RunFileCreation(new FileConfiguration(), txt);
        }
    }
}