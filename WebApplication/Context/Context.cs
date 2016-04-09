namespace WebApplication
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Domain;

    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Mysql", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            DbConfiguration.SetConfiguration(new MySql.Data.Entity.MySqlEFConfiguration());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<UserSkin> UserSkins { get; set; }
    }
}