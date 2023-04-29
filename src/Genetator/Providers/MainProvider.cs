﻿using Generator.Domain;
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
            for (uint chapter = file.StartChapter; chapter <= file.EndChapter; chapter++)
            {
                var url = $"{web.Url}{chapter}/";
                var chapterText = RequestService.GetPageString(url, web.XPathText);
                if (web.HasTitle)
                    file.Title = RequestService.GetPageString(url, web.XPathTitle);

                FileService.RunFileCreation(file, chapterText, chapter);
                Console.WriteLine($"Gerado com sucesso! -> {file.FileName} - Chapter: {chapter}.");
            }
        }

        public async Task Translate(string text) => await _translateService.Translate(text);
    }
}