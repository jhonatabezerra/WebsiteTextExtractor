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
            DependencyConfig.RegisterDependencies(_container); // Configuração das dependências

            _mainProvider = _container.GetInstance<IMainProvider>();
        }

        public void Run(WebConfiguration web, FileConfiguration file)
        {
            //AreaRegistration.RegisterAllAreas();

            // Chame o método Execute
            _mainProvider.Execute(web, file);
        }
    }
}