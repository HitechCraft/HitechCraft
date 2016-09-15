namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives

    using HitechCraft.Core.Entity;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class ShopSaleOverrides : IAutoMappingOverride<ShopSale>
    {
        public void Override(AutoMapping<ShopSale> mapping)
        {
            mapping.Table("ShopSale");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.Map(x => x.Amount)
                .Not.Nullable();

            mapping.References(x => x.Item)
                .Column("Item");
        }
    }
}
