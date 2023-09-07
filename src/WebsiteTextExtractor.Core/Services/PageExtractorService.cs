using WebsiteTextExtractor.Core.Domain.Interfaces;
using WebsiteTextExtractor.Core.Domain.Models;

namespace WebsiteTextExtractor.Core.Services
{
    public class PageExtractorService : IPageExtractorService
    {
        private readonly IRequestService _requestService;

        public PageExtractorService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<List<Chapter>> StartExtractingPages(WebConfiguration web, FileConfiguration file, CancellationToken cancellationToken = default)
        {
            return GetChapters(web, file);
        }

        #region Private Methods

        private List<Chapter> GetChapters(WebConfiguration web, FileConfiguration file)
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

        #endregion Private Methods
    }
}