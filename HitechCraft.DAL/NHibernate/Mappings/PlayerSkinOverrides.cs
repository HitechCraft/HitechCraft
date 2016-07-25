namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class PlayerSkinOverrides : IAutoMappingOverride<PlayerSkin>
    {
        public void Override(AutoMapping<PlayerSkin> mapping)
        {
            mapping.Table("PlayerSkin");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.Map(x => x.Image)
                .Nullable();

            mapping.References(x => x.Player)
                .Column("Player");
        }
    }
}
