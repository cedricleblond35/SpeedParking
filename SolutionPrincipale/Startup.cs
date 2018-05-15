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

                    if (!roleManager.RoleExists(UserRoles.Administrator))
                    {
                        roleManager.Create(new IdentityRole(UserRoles.Administrator));
                        var admin = new ApplicationUser
                        {
                            UserName = "Admin@gmail.com",
                            Email = "Admin@gmail.com"
                        };

                        var result = userManager.Create(admin, "Pa$$w0rd");
                        if (result.Succeeded)
                        {
                            userManager.AddToRole(admin.Id, UserRoles.Administrator);

                            if (!roleManager.RoleExists(UserRoles.Competitor))
                                roleManager.Create(new IdentityRole(UserRoles.Competitor));

                            if (!roleManager.RoleExists(UserRoles.Organizer))
                                roleManager.Create(new IdentityRole(UserRoles.Organizer));

                            transaction.Commit();
                        }
                    }
                }
            }
        }
    }
}
