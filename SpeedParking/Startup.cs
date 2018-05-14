using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpeedParking.Startup))]
namespace SpeedParking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
