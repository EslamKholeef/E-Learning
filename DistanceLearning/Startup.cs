using DistanceLearning.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DistanceLearning.Startup))]
namespace DistanceLearning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            DefaultUser();
        }

        private ApplicationDbContext db = new ApplicationDbContext();
        public void DefaultUser()
        {
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            IdentityRole role = new IdentityRole();

            if (!rolemanager.RoleExists("Admins"))
            {
                role.Name = "Admins";
                rolemanager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "Admin@gmail.com";
                user.UserType = "Admins";

                var check = usermanager.Create(user, "Admin1");
                if (check.Succeeded)
                {
                    usermanager.AddToRole(user.Id, "Admins");
                }

            }

        }
    }
}