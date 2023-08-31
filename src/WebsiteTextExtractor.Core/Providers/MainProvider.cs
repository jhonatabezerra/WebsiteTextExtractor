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
        public void Execute(WebConfiguration web, FileConfiguration file)
        {
            CheckFolders(file);
            GetChapters(web, file);
        }

        public void Execute2(WebConfiguration web, FileConfiguration file)
        {
            GetChapters(web, file);
        }

        public async Task Translate(string text) => await _translateService.Translate(text);

        #region Private Methods

        //private void CreateChapter(WebConfiguration web, FileConfiguration file)
        //{
        //    for (uint chapter = file.StartChapter; chapter <= file.EndChapter; chapter++)
        //    {
        //        var url = $"{web.Url}{chapter}/";
        //        var chapterText = _requestService.GetPageString(url, web.XPathText);
        //        if (web.HasTitle)
        //            file.Title = _requestService.GetPageString(url, web.XPathTitle, web.TagsToFix);

        //        if (string.IsNullOrEmpty(chapterText))
        //        {
        //            Console.WriteLine($"Error when process the Chapter: {chapter}.");
        //            continue;
        //        }

        //        FileService.RunFileCreation(file, chapterText, chapter);
        //        Console.WriteLine($"Success to create the chapter! {file.FileName} - Chapter: {chapter}.");
        //    }
        //}

        private void CheckFolders(FileConfiguration file)
        {
            // TODO: Refactor this method.
            file.Path = FileService.CheckFolderExist(file.Path, FOLDER_BOOKS_GENERATED);
            file.Path = FileService.CheckFolderExist(file.Path, file.FileName);
            file.Path = FileService.CheckFolderExist(file.Path, file.Language);
        }

        public void CheckFolderExistAndCreate(string path, string folderName)
        {
            if (!Directory.Exists($"{path}{folderName}"))
                Directory.CreateDirectory($"{path}{folderName}");
        }

        private void GetChapters(WebConfiguration web, FileConfiguration file)
        {
            List<Chapter> chapterCollection = new();
            for (uint chapter = file.StartChapter; chapter <= file.EndChapter; chapter++)
            {
                var chapterWithContext = GetPageText(web, chapter);
                if (chapterWithContext != null) chapterCollection.Add(chapterWithContext);
            }

            CreateFileWithChapters(chapterCollection, file);
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