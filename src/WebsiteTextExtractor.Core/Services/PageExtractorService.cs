﻿using WebsiteTextExtractor.Core.Domain.Interfaces;
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

        public Task StartExtractingPages(Data data)
        {
            data.Chapters = GetChapters(data);
            return Task.CompletedTask;
        }

        #region Private Methods

        private List<Chapter> GetChapters(Data data)
        {
            List<Chapter> chapterCollection = new();
            for (uint chapter = data.FileConfiguration.StartChapter; chapter <= data.FileConfiguration.EndChapter; chapter++)
            {
                var chapterWithContext = GetPageText(data.WebConfiguration, chapter);
                if (chapterWithContext != null) chapterCollection.Add(chapterWithContext);
            }
            return chapterCollection;
        }

        // TODO: Refactor this method - Is not neccessary call 'GetPageString' 2 times.
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