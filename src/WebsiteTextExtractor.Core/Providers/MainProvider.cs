using WebsiteTextExtractor.Core.Domain;
using WebsiteTextExtractor.Core.Services;

namespace WebsiteTextExtractor.Core.Providers
{
    public class MainProvider
    {
        private const string FOLDER_BOOKS_GENERATED = "BooksGenerated";

        private readonly TranslatePageService _translateService;
        private readonly RequestService _requestService;

        public MainProvider()
        {
            _translateService = new TranslatePageService();
            _requestService = new RequestService();
        }

        /// <summary>Execute the process to export chapter.</summary>
        public async Task Execute(WebConfiguration web, FileConfiguration file)
        {
            CheckFolders(file);

            var chapterCollection = await GetChapters(web, file);
            CreateFileWithChapters(chapterCollection, file);
        }

        public async Task Translate(string text) => await _translateService.Translate(text);

        #region Private Methods

        private void CheckFolders(FileConfiguration file)
        {
            // TODO: Refactor this method.
            file.Path = FileService.CheckFolderExist(file.Path, FOLDER_BOOKS_GENERATED);
            file.Path = FileService.CheckFolderExist(file.Path, file.FileName);
            file.Path = FileService.CheckFolderExist(file.Path, file.Language);
        }

        private async Task<List<Chapter>> GetChapters(WebConfiguration web, FileConfiguration file)
        {
            List<Chapter> chapterCollection = new();
            for (uint chapter = file.StartChapter; chapter <= file.EndChapter; chapter++)
            {
                var chapterWithContext = GetPageText(web, chapter);
                if (chapterWithContext != null) chapterCollection.Add(chapterWithContext);
            }
            return chapterCollection;
        }

        private Chapter? GetPageText(WebConfiguration web, uint chapter)
        {
            var url = $"{web.Url}{chapter}/";
            var chapterTitle = _requestService.GetPageString(url, web.XPathTitle);
            var chapterText = _requestService.GetPageString(url, web.XPathText, web.TagsToFix);
            if (string.IsNullOrEmpty(chapterText))
            {
                Console.WriteLine($"Error when proccess the Chapter: {chapter}.");
                return null;
            }
            Console.WriteLine($"Success to process the Chapter: {chapter}.");
            return new Chapter(chapter, $"The Cursed Prince - {chapterTitle}", chapterText);
        }

        private void CreateFileWithChapters(List<Chapter> chapterCollection, FileConfiguration file)
        {
            FileService.RunCreateBigBook(file, chapterCollection);
            Console.WriteLine($"Created the book: {file.FileName} - Chapter: {file.StartChapter} to {file.EndChapter}.");
        }

        #endregion Private Methods
    }
}