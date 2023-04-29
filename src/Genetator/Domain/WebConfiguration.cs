namespace Generator.Domain
{
    /// <summary>Respresents the WEB site configuration.</summary>
    public class WebConfiguration
    {
        public WebConfiguration(string url, string? xPath = null, string? xPathTitle = null, bool hasTitle = false)
        {
            Url = url;
            XPathText = xPath;
            XPathTitle = xPathTitle;
            HasTitle = hasTitle;
        }

        public string Url { get; set; }
        public string? XPathText { get; set; }
        public string? XPathTitle { get; set; }
        public bool HasTitle { get; set; }
    }
}