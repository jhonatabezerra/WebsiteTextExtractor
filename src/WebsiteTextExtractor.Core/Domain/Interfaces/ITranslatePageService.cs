namespace WebsiteTextExtractor.Core.Domain.Interfaces
{
    public interface ITranslatePageService
    {
        Task<string> Translate(string text);
    }
}