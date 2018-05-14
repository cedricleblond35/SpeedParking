using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BO.Startup))]
namespace BO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
