namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class ShopItemCategoryOverrides : IAutoMappingOverride<ShopItemCategory>
    {
        public void Override(AutoMapping<ShopItemCategory> mapping)
        {
            mapping.Table("ShopItemCategory");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.Map(x => x.Name)
                .Not.Nullable();

            mapping.Map(x => x.Description);
        }
    }
}
