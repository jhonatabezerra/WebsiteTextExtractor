using WebsiteTextExtractor.Core.Domain.Models;

namespace WebsiteTextExtractor.Core.Domain.Interfaces
{
    public interface IPageExtractorService
    {
        Task<List<Chapter>> StartExtractingPages(WebConfiguration web, FileConfiguration file, CancellationToken cancellationToken = default);
    }
}