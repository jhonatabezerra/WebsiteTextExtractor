using WebsiteTextExtractor.Core.Domain.Models;

namespace WebsiteTextExtractor.Core.Domain.Interfaces
{
    public interface IMainProvider
    {
        Task Execute(WebConfiguration web, FileConfiguration file, CancellationToken cancellationToken = default);

        Task Translate(string text);
    }
}