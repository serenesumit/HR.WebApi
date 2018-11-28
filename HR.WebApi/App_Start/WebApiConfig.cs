using HR.WebApi.Helpers;
using MultipartDataMediaFormatter;
using MultipartDataMediaFormatter.Infrastructure;
using System.Web.Http;

namespace HR.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           
            // Web API configuration and services=
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();
            GlobalConfiguration.Configuration.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter(new MultipartFormatterSettings()));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new ElmahErrorAttribute());
        }
    }
}
