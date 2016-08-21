namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using Common.Models.Enum;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class PMPlayerOverrides : IAutoMappingOverride<PMPlayer>
    {
        public void Override(AutoMapping<PMPlayer> mapping)
        {
            mapping.Table("PMPlayer");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.References(x => x.Message)
                .Column("Message")
                .Not.Nullable();

            mapping.References(x => x.Player)
                .Column("Player")
                .Not.Nullable();

            mapping.Map(x => x.PlayerType)
                .CustomType<PMPlayerType>();

            mapping.Map(x => x.PmType)
                .CustomType<PMType>();
        }
    }
}
