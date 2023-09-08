namespace WebsiteTextExtractor.Core.Domain.Models
{
    public class Data
    {
        public WebConfiguration WebConfiguration { get; set; }
        public FileConfiguration FileConfiguration { get; set; }
        public List<Chapter>? Chapters { get; set; }

        public Data(
            WebConfiguration webConfiguration,
            FileConfiguration fileConfiguration)
        {
            WebConfiguration = webConfiguration;
            FileConfiguration = fileConfiguration;
        }
    }
}