using WebsiteTextExtractor.Core.Domain.Models;
using Xunit;

namespace WebsiteTextExtractor.Core.Tests
{
    public class StartupTests
    {
        private readonly Startup _program;

        public StartupTests() => _program = new Startup();

        [Fact]
        public void Run()
        {
            // Arrange
            const string URL = "https://novelbin.net/n/the-cursed-prince/chapter-";
            const string XPATH = "//*[@id=\"chr-content\"]";
            const string XPATH_TITLE = "//*[@id=\"chapter\"]/div/div/h2/a";
            const string FILE_NAME = "The Cursed Prince";
            const string LANGUAGE = "EN";
            const string PATH = "C:\\Users\\Jhonata\\Documents";
            const uint START_CHAPTER = 1;
            const uint END_CHAPTER = 10;

            WebConfiguration web = new(URL, XPATH, XPATH_TITLE);
            FileConfiguration file = new(FILE_NAME, LANGUAGE, PATH, START_CHAPTER, END_CHAPTER);

            // Act
            _program.Run(web, file);

            // Assert
        }
    }
}