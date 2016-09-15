namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives

    using HitechCraft.Core.Entity;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class ShopItemOverrides : IAutoMappingOverride<ShopItem>
    {
        public void Override(AutoMapping<ShopItem> mapping)
        {
            mapping.Table("ShopItem");

            mapping.Map(x => x.Id)
                .Generated.Insert();

            mapping.Id(x => x.GameId);

            mapping.Map(x => x.Name)
                .Length(128)
                .Not.Nullable();

            mapping.Map(x => x.Description)
                .Length(2000);

            mapping.Map(x => x.Price);

            mapping.Map(x => x.Image);

            mapping.References(x => x.Modification)
                .Column("Modification");

            mapping.References(x => x.ItemCategory)
                .Column("Category");
        }
    }
}
