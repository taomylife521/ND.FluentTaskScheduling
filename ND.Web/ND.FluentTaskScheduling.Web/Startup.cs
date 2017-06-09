using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ND.FluentTaskScheduling.Web.Startup))]
namespace ND.FluentTaskScheduling.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
