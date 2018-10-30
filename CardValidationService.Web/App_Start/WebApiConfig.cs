using System.Web.Http;

namespace CardValidationService.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "CardValidationApi",
                routeTemplate: "api/{action}",
                defaults: new { controller = "CardValidationApi" }
            );

            config.Routes.MapHttpRoute(
                name: "StartUri",
                routeTemplate: "",
                defaults: new { controller = "CardValidationApi", action = "Index" }
            );
        }
    }
}
