using Generator.Domain;
using Generator.Services;

namespace Generator.Providers
{
    public class MainProvider
    {
        private const string FOLDER_BOOKS_GENERATED = "BooksGenerated";

        private readonly TranslatePageService _translateService;

        public MainProvider() => _translateService = new TranslatePageService();

        /// <summary>Execute the process to export chapter.</summary>
        public void Execute(WebConfiguration web, FileConfiguration file)
        {
            file.Path = FileService.CheckFolderExist(file.Path, FOLDER_BOOKS_GENERATED);
            file.Path = FileService.CheckFolderExist(file.Path, file.FileName);
            file.Path = FileService.CheckFolderExist(file.Path, file.Language);

            if (file.IsBigBook) CreateBook(web, file);
            else CreateChapter(web, file);
        }

        public async Task Translate(string text) => await _translateService.Translate(text);

        #region Private Methods

        private void CreateChapter(WebConfiguration web, FileConfiguration file)
        {
            for (uint chapter = file.StartChapter; chapter <= file.EndChapter; chapter++)
            {
                var url = $"{web.Url}{chapter}/";
                var chapterText = RequestService.GetPageString(url, web.XPathText);
                if (web.HasTitle)
                    file.Title = RequestService.GetPageString(url, web.XPathTitle, web.TagsToFix);

                if (string.IsNullOrEmpty(chapterText))
                {
                    Console.WriteLine($"Error when process the Chapter: {chapter}.");
                    continue;
                }

                FileService.RunFileCreation(file, chapterText, chapter);
                Console.WriteLine($"Success to create the chapter! {file.FileName} - Chapter: {chapter}.");
            }
        }

        private void CreateBook(WebConfiguration web, FileConfiguration file)
        {
            List<Chapter> chapters = new();
            for (uint chapter = file.StartChapter; chapter <= file.EndChapter; chapter++)
            {
                var url = $"{web.Url}{chapter}/";
                var chapterTitle = RequestService.GetPageString(url, web.XPathTitle);
                var chapterText = RequestService.GetPageString(url, web.XPathText, web.TagsToFix);
                if (string.IsNullOrEmpty(chapterText))
                {
                    Console.WriteLine($"Error when proccess the Chapter: {chapter}.");
                    continue;
                }
                chapters.Add(new Chapter(chapter, $"The Cursed Prince - {chapterTitle}", chapterText));
                Console.WriteLine($"Success to process the Chapter: {chapter}.");
            }

            FileService.RunCreateBigBook(file, chapters);
            Console.WriteLine($"Created the book: {file.FileName} - Chapter: {file.StartChapter} to {file.EndChapter}.");
        }

        #endregion Private Methods
    }
}