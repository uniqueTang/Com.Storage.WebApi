
using System.Web.Http;
using System.Web.Http.Cors;

namespace Com.Storage.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // 配置跨域
            config.EnableCors(new EnableCorsAttribute("http://127.0.0.1","*","*"));
        }
    }
}
