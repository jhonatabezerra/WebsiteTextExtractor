using WebsiteTextExtractor.Core.Domain;
using WebsiteTextExtractor.Core.Providers;
using Xunit;

namespace WebsiteTextExtractor.Core.Tests.Providers
{
    public class MainProviderTests
    {
        private readonly MainProvider _mainProvider;

        public MainProviderTests()
        {
            _mainProvider = new MainProvider();
        }

        [Fact]
        public void Ctor()
        {
            // Arrange
            WebConfiguration web;
            FileConfiguration file;

            // Act
            _mainProvider.Execute(web, file);

            // Assert
        }
    }
}