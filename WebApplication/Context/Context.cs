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

        public DbSet<Player> Players { get; set; }

        public DbSet<UserSkin> UserSkins { get; set; }

        public DbSet<PlayerItem> PlayerItems { get; set; }

        public DbSet<ShopItem> ShopItems { get; set; }

        public DbSet<ShopSale> ShopSales { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Ban> Bans { get; set; }

        public DbSet<BanIp> BanIps { get; set; }

        public DbSet<PlayerSession> PlayerSessions { get; set; }
    }
}