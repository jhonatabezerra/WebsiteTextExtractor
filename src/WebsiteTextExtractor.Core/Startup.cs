using SimpleInjector;
using WebsiteTextExtractor.Core.Configuration;
using WebsiteTextExtractor.Core.Domain.Interfaces;
using WebsiteTextExtractor.Core.Domain.Models;

namespace WebsiteTextExtractor.Core
{
    public class Startup
    {
        private readonly IMainProvider _mainProvider;
        private readonly Container _container = new();

        public Startup()
        {
            DependencyConfig.RegisterDependencies(_container);

            _mainProvider = _container.GetInstance<IMainProvider>();
        }

        public void Run(Data data)
        {
            _mainProvider.Execute(data);
        }
    }
}