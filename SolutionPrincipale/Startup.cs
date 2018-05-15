using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SolutionPrincipale.Startup))]
namespace SolutionPrincipale
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
