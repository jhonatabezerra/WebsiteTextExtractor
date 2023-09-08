using WebsiteTextExtractor.Core.Domain.Models;

namespace WebsiteTextExtractor.Core.Tests
{
    public class BaseTests
    {
        private const string URL = "https://novelbin.net/n/the-cursed-prince/chapter-";
        private const string XPATH = "//*[@id=\"chr-content\"]";
        private const string XPATH_TITLE = "//*[@id=\"chapter\"]/div/div/h2/a";
        private const string FILE_NAME = "The Cursed Prince";
        private const string LANGUAGE = "EN";
        private const string PATH = "C:\\Users\\Jhonata\\Documents";
        private const uint START_CHAPTER = 1;
        private const uint END_CHAPTER = 10;

        internal readonly Data _data = new(new(URL, XPATH, XPATH_TITLE), new(FILE_NAME, LANGUAGE, PATH, START_CHAPTER, END_CHAPTER));
    }
}