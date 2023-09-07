namespace WebsiteTextExtractor.Core.Domain.Interfaces
{
    public interface IRequestService
    {
        string GetPageString(string url, string xPath, List<string>? tags = null);
    }
}