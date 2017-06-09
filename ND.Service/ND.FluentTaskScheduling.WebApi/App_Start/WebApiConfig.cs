using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ND.FluentTaskScheduling.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            config.Routes.Clear();
            // Web API 路由
            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApiName",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
