using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CityParkWeb.Startup))]
namespace CityParkWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
