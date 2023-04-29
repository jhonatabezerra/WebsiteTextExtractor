namespace Generator.Domain
{
    /// <summary>Respresents the WEB site configuration.</summary>
    public class WebConfiguration
    {
        public WebConfiguration(string url, string xPath)
        {
            Url = url;
            XPath = xPath;
        }

        public string Url { get; set; }
        public string XPath { get; set; }
    }
}