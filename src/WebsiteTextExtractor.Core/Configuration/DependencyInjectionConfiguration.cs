using SimpleInjector;
using WebsiteTextExtractor.Core.Domain.Interfaces;
using WebsiteTextExtractor.Core.Providers;
using WebsiteTextExtractor.Core.Services;

namespace WebsiteTextExtractor.Core.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterDependencies(Container container)
        {
            // Services
            container.Register<IFileService, FileService>();
            container.Register<IRequestService, RequestService>();
            container.Register<ITranslatePageService, TranslatePageService>();
            container.Register<IPageExtractorService, PageExtractorService>();

            // Providers
            container.Register<IMainProvider, MainProvider>();
            container.Register<IDirectoryProvider, DirectoryProvider>();

            //DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            container.Verify();
        }
    }
}