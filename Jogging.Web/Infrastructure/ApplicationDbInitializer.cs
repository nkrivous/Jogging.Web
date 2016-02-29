using System.Data.Entity;
using Jogging.Web.Models;
using Jogging.Web.Utils.Constants;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Jogging.Web.Infrastructure
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //create roles
            var adminRoleName = Role.Admin.ToString();
            if (!roleManager.RoleExists(adminRoleName))
            {
                roleManager.Create(new IdentityRole(adminRoleName));
            }

            var userRoleName = Role.Admin.ToString();
            if (!roleManager.RoleExists(userRoleName))
            {
                roleManager.Create(new IdentityRole(userRoleName));
            }

            //create users
            var user = new ApplicationUser();
            user.UserName = "admin";
            user.Email = "admin@example.com";
            string password = "123456";
            var adminresult = userManager.Create(user, password);

            if (adminresult.Succeeded)
            {
                userManager.AddToRole(user.Id, adminRoleName);
            }
            base.Seed(db);

        }
    }
}