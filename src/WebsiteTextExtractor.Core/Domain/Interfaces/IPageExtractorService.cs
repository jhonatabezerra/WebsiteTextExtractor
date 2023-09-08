using WebsiteTextExtractor.Core.Domain.Models;

namespace WebsiteTextExtractor.Core.Domain.Interfaces
{
    public interface IPageExtractorService
    {
        Task StartExtractingPages(Data data);
    }
}