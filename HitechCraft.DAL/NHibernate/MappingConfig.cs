namespace HitechCraft.DAL.NHibernate
{
    #region Using Directives

    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using Helper;
    using Domain;
    using Mappings;

    #endregion

    /// <summary>
    /// Mappings for NHibernate entities
    /// Nice mapping info https://github.com/jagregory/fluent-nhibernate/wiki/Fluent-mapping
    /// </summary>
    public static class MappingConfig
    {
        public static AutoMappingsContainer AutoMappings(this MappingConfiguration mapConfig, AutomappingHelper autoMapHelper)
        {
            return mapConfig.AutoMappings
                .Add(AutoMap.AssemblyOf<AuthLog>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<AuthLogOverrides>())
                .Add(AutoMap.AssemblyOf<Ban>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<BanOverrides>())
                .Add(AutoMap.AssemblyOf<BanIp>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<BanIpOverrides>()) 
                .Add(AutoMap.AssemblyOf<Modification>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<ModificationOverrides>())
                .Add(AutoMap.AssemblyOf<Server>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<ServerOverrides>())
                .Add(AutoMap.AssemblyOf<ServerModification>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<ServerModificationOverrides>())
                .Add(AutoMap.AssemblyOf<News>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<NewsOverrides>())
                .Add(AutoMap.AssemblyOf<Comment>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<CommentOverrides>())
                .Add(AutoMap.AssemblyOf<Job>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<JobOverrides>())
                .Add(AutoMap.AssemblyOf<Currency>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<CurrencyOverrides>())
                .Add(AutoMap.AssemblyOf<Player>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<PlayerOverrides>())
                .Add(AutoMap.AssemblyOf<Skin>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<SkinOverrides>())
                .Add(AutoMap.AssemblyOf<PlayerInfo>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<PlayerInfoOverrides>())
                .Add(AutoMap.AssemblyOf<PlayerSession>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<PlayerSessionOverrides>())
                .Add(AutoMap.AssemblyOf<PlayerSkin>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<PlayerSkinOverrides>())
                .Add(AutoMap.AssemblyOf<PlayerItem>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<PlayerItemOverrides>())
                .Add(AutoMap.AssemblyOf<ShopItem>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<ShopItemOverrides>())
                .Add(AutoMap.AssemblyOf<ShopItemCategory>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<ShopItemCategoryOverrides>())
                .Add(AutoMap.AssemblyOf<ShopSale>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<ShopSaleOverrides>())
                .Add(AutoMap.AssemblyOf<IKTransaction>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<IKTransactionOverrides>())
                .Add(AutoMap.AssemblyOf<Permissions>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<PermissionsOverrides>())
                .Add(AutoMap.AssemblyOf<PexEntity>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<PexEntityOverrides>())
                .Add(AutoMap.AssemblyOf<PexInheritance>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<PexInheritanceOverrides>())
                .Add(AutoMap.AssemblyOf<Rule>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<RuleOverrides>())
                .Add(AutoMap.AssemblyOf<RulePoint>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<RulePointOverrides>())
                .Add(AutoMap.AssemblyOf<PrivateMessage>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<PrivateMessageOverrides>())
                .Add(AutoMap.AssemblyOf<PMPlayer>(autoMapHelper)
                    .UseOverridesFromAssemblyOf<PMPlayerOverrides>());
        }
    }
}
