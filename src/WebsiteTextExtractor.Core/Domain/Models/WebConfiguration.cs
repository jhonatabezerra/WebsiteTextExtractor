namespace WebsiteTextExtractor.Core.Domain.Models
{
    /// <summary>Respresents the WEB site configuration.</summary>
    public class WebConfiguration
    {
        public WebConfiguration(
            string url,
            string xPath = "",
            string xPathTitle = "",
            bool hasTitle = false,
            List<string>? tagsToFix = null)
        {
            Url = url;
            XPathText = xPath;
            XPathTitle = xPathTitle;
            HasTitle = hasTitle;
            TagsToFix = tagsToFix;
        }

        public string Url { get; set; }
        public string XPathText { get; set; }
        public string XPathTitle { get; set; }
        public bool HasTitle { get; set; }
        public List<string>? TagsToFix { get; set; }
    }

    public class Chapter
    {
        public Chapter(uint chapterNumber, string chapterTitle, string chapterText)
        {
            ChapterNumber = chapterNumber;
            ChapterTitle = chapterTitle;
            ChapterText = chapterText;
        }

        public uint ChapterNumber { get; set; }
        public string ChapterText { get; set; }
        public string ChapterTitle { get; set; }
    }
}