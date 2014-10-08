using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HCCADBWebAppPrototype1.Startup))]
namespace HCCADBWebAppPrototype1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
