using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SolutionPrincipale.Models;

[assembly: OwinStartupAttribute(typeof(SolutionPrincipale.Startup))]
namespace SolutionPrincipale
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            using (var context = new ApplicationDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var roleManager = new RoleManager<IdentityRole>
                    (new RoleStore<IdentityRole>(context));

                    var userManager = new UserManager<ApplicationUser>
                        (new UserStore<ApplicationUser>(context));

                    if (!roleManager.RoleExists(BO.Constantes.Administrateur))
                    {
                        roleManager.Create(new IdentityRole(BO.Constantes.Administrateur));
                        var admin = new ApplicationUser
                        {
                            UserName = "Admin@gmail.com",
                            Email = "Admin@gmail.com"
                        };

                        var result = userManager.Create(admin, "Pa$$w0rd");
                        if (result.Succeeded)
                        {
                            userManager.AddToRole(admin.Id, BO.Constantes.Administrateur);

                            if (!roleManager.RoleExists(BO.Constantes.Convive))
                                roleManager.Create(new IdentityRole(BO.Constantes.Convive));

                            if (!roleManager.RoleExists(BO.Constantes.Organisateur))
                                roleManager.Create(new IdentityRole(BO.Constantes.Organisateur));

                            userManager.AddToRole(admin.Id, BO.Constantes.Organisateur);

                            transaction.Commit();
                        }
                    }
                }
            }
        }
    }
}
