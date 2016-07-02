namespace WebApplication
{
    #region Using Directives

    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Domain;
    using Domain.PermissionsEx;

    #endregion

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

        #region Domain DbSets

        public DbSet<Player> Players { get; set; }

        public DbSet<UserSkin> UserSkins { get; set; }

        public DbSet<PlayerItem> PlayerItems { get; set; }

        public DbSet<ShopItem> ShopItems { get; set; }

        public DbSet<ShopSale> ShopSales { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Ban> Bans { get; set; }

        public DbSet<BanIp> BanIps { get; set; }

        public DbSet<PlayerSession> PlayerSessions { get; set; }

        public DbSet<Server> Servers { get; set; }

        public DbSet<Modification> Modifications { get; set; }

        public DbSet<ServerModification> ServerModifications { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PexEntity> PexEntities { get; set; }

        public DbSet<PexInheritance> PexInheritances { get; set; }

        public DbSet<IKTransaction> IkTransactions { get; set; }

        #endregion
    }
}