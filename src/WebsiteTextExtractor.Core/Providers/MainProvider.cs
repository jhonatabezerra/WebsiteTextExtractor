using WebsiteTextExtractor.Core.Domain.Interfaces;
using WebsiteTextExtractor.Core.Domain.Models;

namespace WebsiteTextExtractor.Core.Providers
{
    public class MainProvider : IMainProvider
    {
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
        public async Task Execute(Data data)
        {
            await _fileService.CheckFolders(data);
            await _pageExtractorService.StartExtractingPages(data);
            //await _translatePageService.Translate(text); // TODO: fix this method.
            await _fileService.StartCreatingFiles(data);
        }
    }
}