
using FluentValidation.WebApi;
using ND.FluentTaskScheduling.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ND.FluentTaskScheduling.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            HttpConfiguration config = new HttpConfiguration();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // 使api返回为json 
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            AutoFacBootStrapper.CoreAutoFacInit();
          
            //Swashbuckle.Bootstrapper.Init(config);
            SwaggerConfig.Register();
            config.Filters.Add(new WebApiValidateActionFilterAttribute());
           
          
        }
    }
}
