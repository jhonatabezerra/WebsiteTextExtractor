using HtmlAgilityPack;
using System.Web;

namespace ConsoleApp1.Services
{
    /// <summary>Represents the Request services (GET, POST...).</summary>
    public class RequestService
    {
        /// <summary>Request page.</summary>
        /// <param name="url">Used to get the HTML body.</param>
        /// <returns>String of page.</returns>
        public string GetPageString(string url, string xPath)
        {
            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(url);
                var text = doc.DocumentNode.SelectSingleNode(xPath).InnerText.Trim();

                string decodedText = HttpUtility.HtmlDecode(text);
                return decodedText;
            }
            catch (Exception exeption)
            {
                Console.WriteLine($"Error - {url}.");
                Console.WriteLine(exeption.Message);
                throw;
            }
        }
    }
}