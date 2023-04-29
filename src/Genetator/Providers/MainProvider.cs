using ConsoleApp1.Domain;
using ConsoleApp1.Services;

namespace ConsoleApp1.Providers
{
    public class MainProvider
    {
        private const string PREFIX_TYPE = "txt";
        private readonly RequestService _service;
        private readonly FileService _fileService;
        private readonly TranslatePageService _translateService;

        public MainProvider()
        {
            _service = new RequestService();
            _fileService = new FileService();
            _translateService = new TranslatePageService();
        }

        /// <summary>Execute the process to export chapter.</summary>
        public void Execute()
        {
            var chapter = 1498;
            var language = "EN";
            var fileName = "Release that Witch";
            var path = @"C:\Users\Jhonata\Documents\BooksGenerated\";
            var fileConfig = ConfigureFile(fileName, language, path, chapter);
            var chapterText = GetChapterText(chapter);
            _fileService.RunFileCreation(fileConfig, chapterText);
        }

        public void ExecuteAll(int startChapter, int chapterQuantity)
        {
            GenerateCollection(startChapter, chapterQuantity);
        }

        public async Task Translate(string text) => await _translateService.Translate(text);

        #region Private Methods

        private void GenerateCollection(int startChapter, int chapterQuantity)
        {
            for (int chapter = startChapter; chapter <= chapterQuantity; chapter++)
            {
                var language = "EN";
                var fileName = "Release that Witch";
                var path = @"C:\Users\Jhonata\Documents\BooksGenerated\";
                FileConfiguration fileConfigEN = ConfigureFile(fileName, language, path, chapter);
                var chapterText = GetChapterText(chapter);
                _fileService.RunFileCreation(fileConfigEN, chapterText);
                Console.WriteLine($"Gerado com sucesso! -> {fileConfigEN.FileName} - Chapter: {chapter}.");
                //var translatedChapterText = await _translateService.Translate(chapterText);
                //_fileService.RunFileCreation(fileConfigPT, translatedChapterText);
            }
        }

        private string GetChapterText(int capter)
        {
            var url = $"https://boxnovel.com/novel/release-that-witch/chapter-{capter}/";
            var xPath = "//div[contains(@class, 'text-left')]";
            var text = _service.GetPageString(url, xPath);

            return text;
        }

        private static FileConfiguration ConfigureFile(string fileName, string language, string path, int chapter) => new()
        {
            FileName = fileName,
            Path = path,
            Chapter = chapter.ToString(),
            Language = language,
            Type = PREFIX_TYPE
        };

        #endregion Private Methods
    }
}