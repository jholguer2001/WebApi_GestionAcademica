using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi_GestionAcademica
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configurar CORS - incluir específicamente el origen HTTPS
            var corsAttr = new EnableCorsAttribute(
                origins: "http://localhost:3000,https://localhost:3000",
                headers: "*",
                methods: "*");
            config.EnableCors(corsAttr);

            // Configurar serializador JSON para ignorar referencias circulares
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Rutas de API web
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}