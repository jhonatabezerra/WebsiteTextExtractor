using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;
using WebsiteTextExtractor.Core.Domain.Interfaces;
using WebsiteTextExtractor.Core.Providers;
using WebsiteTextExtractor.Core.Services;

namespace WebsiteTextExtractor.Core.Configuration
{
    public static class DependencyConfig
    {
        public static void RegisterDependencies(Container container)
        {
            container.Register<IFileService, FileService>();
            container.Register<IMainProvider, MainProvider>();
            container.Register<IRequestService, RequestService>();
            container.Register<ITranslatePageService, TranslatePageService>();
            container.Register<IPageExtractorService, PageExtractorService>();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            container.Verify();
        }
    }
}