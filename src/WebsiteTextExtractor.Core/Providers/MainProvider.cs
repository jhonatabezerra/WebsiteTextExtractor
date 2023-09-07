using WebsiteTextExtractor.Core.Domain.Interfaces;
using WebsiteTextExtractor.Core.Domain.Models;

namespace WebsiteTextExtractor.Core.Providers
{
    public class MainProvider : IMainProvider
    {
        private const string FOLDER_BOOKS_GENERATED = "BooksGenerated";

        private readonly ITranslatePageService _translatePageService;
        private readonly IPageExtractorService _pageExtractorService;
        private readonly IFileService _fileService;

        public MainProvider(
            ITranslatePageService translatePageService,
            IPageExtractorService pageExtractorService,
            IFileService fileService)
        {
            _translatePageService = translatePageService;
            _pageExtractorService = pageExtractorService;
            _fileService = fileService;
        }

        /// <summary>Execute the process to export chapter.</summary>
        public async Task Execute(WebConfiguration web, FileConfiguration file, CancellationToken cancellationToken = default)
        {
            CheckFolders(file);

            var chapterCollection = await _pageExtractorService.StartExtractingPages(web, file, cancellationToken);
            _fileService.StartCreatingFiles(file, chapterCollection);
        }

        public async Task Translate(string text) => await _translatePageService.Translate(text);

        #region Private Methods

        private void CheckFolders(FileConfiguration file)
        {
            // TODO: Refactor this method.
            file.Path = _fileService.CheckFolderExist(file.Path, FOLDER_BOOKS_GENERATED);
            file.Path = _fileService.CheckFolderExist(file.Path, file.FileName);
            file.Path = _fileService.CheckFolderExist(file.Path, file.Language);
        }

        #endregion Private Methods
    }
}