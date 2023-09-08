using Xunit;

namespace WebsiteTextExtractor.Core.Tests
{
    public class StartupTests : BaseTests
    {
        private readonly Startup _program;

        public StartupTests() => _program = new Startup();

        [Fact]
        public void Run_WhenCall_ShouldRetunsAllProccess()
        {
            // Arrange

            // Act
            _program.Run(_data);

            // Assert
        }
    }
}