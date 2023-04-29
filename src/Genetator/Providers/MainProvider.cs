using Generator.Domain;
using Generator.Services;

namespace Generator.Providers
{
    public class MainProvider
    {
        private readonly RequestService _service;
        private readonly TranslatePageService _translateService;

        public MainProvider()
        {
            _service = new RequestService();
            _translateService = new TranslatePageService();
        }

        /// <summary>Execute the process to export chapter.</summary>
        public void Execute()
        {
            uint startChapter = 1;
            uint endChapter = 10;
            var language = "EN";
            var fileName = "Release that Witch";
            var path = @"C:\Users\Jhonata\Documents\";

            var initialUrl = $"https://boxnovel.com/novel/release-that-witch/chapter-";
            var xPath = "//div[contains(@class, 'text-left')]";

            WebConfiguration webConfig = new(initialUrl, xPath);
            FileConfiguration fileConfig = new(fileName, language, path, startChapter, endChapter);

            GenerateCollection(webConfig, fileConfig);
        }

        public async Task Translate(string text) => await _translateService.Translate(text);

        #region Private Methods

        private void GenerateCollection(WebConfiguration web, FileConfiguration file)
        {
            for (uint chapter = file.StartChapter; chapter <= file.EndChapter; chapter++)
            {
                var url = $"{web.Url}{chapter}/";
                var chapterText = _service.GetPageString(url, web.XPath);
                FileService.RunFileCreation(file, chapterText, chapter);
                Console.WriteLine($"Gerado com sucesso! -> {file.FileName} - Chapter: {chapter}.");
            }
        }

        #endregion Private Methods
    }
}