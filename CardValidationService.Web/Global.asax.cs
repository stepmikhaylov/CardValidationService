using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace CardValidationService.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
