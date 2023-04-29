using ConsoleApp1.Domain;
using ConsoleApp1.Services;
using System.Text.RegularExpressions;

namespace ConsoleApp1.Providers
{
    public class MainProvider
    {
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
                //FileConfiguration fileConfigPT = ConfigureFile(languagePT, chapter);
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
            //var text = CutHttp(httpString, capter); TODO: Old way.

            return text;
        }

        private string CutHttp(string chapterCollection, int index)
        {
            var split1 = chapterCollection.Split($"value=\"chapter-{index}\" />");
            var split2 = split1[1].Split("</script></div>\r\n</div>");
            var split3 = split2[0].Split("<p>", 2);
            var firstBody = split3[1];
            var cutBody = RemoveSpecificTag(firstBody);
            var split4 = cutBody.Split("<div id=\"text-chapter-toolbar\">");

            var finallyBody = RemoveAllTagsHtml(split4[0]);

            return finallyBody;
        }

        private static string RemoveSpecificTag(string body)
        {
            var newBody = body;

            newBody = RemoveTag(newBody, "<p>");
            newBody = RemoveTag(newBody, "</p>");
            newBody = RemoveIndex(newBody, 6);

            return newBody;
        }

        private static string RemoveTag(string text, string caracter) =>
            text.Replace(caracter, string.Empty);

        private static string RemoveIndex(string text, int index)
        {
            int position = text.IndexOf("&#");
            var removePosition = text.Substring(position, index);
            var newString = text.Replace(removePosition, string.Empty);
            return newString;
        }

        private static string RemoveAllTagsHtml(string input) => Regex.Replace(input, @"<[^>]*>", String.Empty);

        private static FileConfiguration ConfigureFile(string fileName, string language, string path, int chapter) => new()
        {
            FileName = fileName,
            Path = path,
            Chapter = chapter.ToString(),
            Language = language,
            Type = "txt"
        };

        #endregion Private Methods
    }
}