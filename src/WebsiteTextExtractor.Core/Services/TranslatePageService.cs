using Flurl.Http;
using System.Web;

namespace WebsiteTextExtractor.Core.Services
{
    /// <summary>Represents the Translate pages service.</summary>
    public class TranslatePageService
    {
        /// <summary>Translate the text.</summary>
        /// <param name="text">The text that will be translated.</param>
        /// <returns>The translated text.</returns>
        public async Task<string> Translate(string text)
        {
            var input = text;
            var fromLanguage = "en";
            var toLanguage = "pt";
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(input)}";

            var clientWithHeader = await url.AllowAnyHttpStatus().GetAsync();
            var content = await clientWithHeader.GetStringAsync();

            return content;
        }
    }
}