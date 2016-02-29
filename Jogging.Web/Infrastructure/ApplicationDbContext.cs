using System.Data.Entity;
using Jogging.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Jogging.Web.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("JoggingConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        public DbSet<JoggingItem> Joggings { get; set; }
    }
}