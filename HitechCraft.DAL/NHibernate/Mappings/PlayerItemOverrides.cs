namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class PlayerItemOverrides : IAutoMappingOverride<PlayerItem>
    {
        public void Override(AutoMapping<PlayerItem> mapping)
        {
            mapping.Table("PlayerItem");

            mapping.Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Increment();

            mapping.References(x => x.Player)
                .Column("nickname");

            mapping.References(x => x.Item)
                .Column("item_id");

            mapping.Map(x => x.Count)
                .Length(11)
                .Column("item_amount");
        }
    }
}
