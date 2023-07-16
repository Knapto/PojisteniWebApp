using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PojisteniWebApp.Startup))]
namespace PojisteniWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
