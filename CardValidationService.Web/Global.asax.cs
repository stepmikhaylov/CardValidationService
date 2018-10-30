using System.Web;
using System.Web.Http;
using CardValidationService.Repositories;
using Unity;

namespace CardValidationService.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(new UnityContainer()
                .RegisterType<ICardValidationServiceRepository, CardValidationServiceRepository>());
        }
    }
}
