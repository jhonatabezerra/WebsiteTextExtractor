using WebsiteTextExtractor.Core.Domain.Models;

namespace WebsiteTextExtractor.Core.Domain.Interfaces
{
    public interface IMainProvider
    {
        Task Execute(Data data);
    }
}