using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SKH_GS_MVC.Startup))]
namespace SKH_GS_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
