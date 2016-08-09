namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class PlayerSessionOverrides : IAutoMappingOverride<PlayerSession>
    {
        public void Override(AutoMapping<PlayerSession> mapping)
        {
            mapping.Table("PlayerSession");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.References(x => x.Player)
                .Column("Player");

            mapping.Map(x => x.Session)
                .Length(255);

            mapping.Map(x => x.Server)
                .Length(255);

            mapping.Map(x => x.Token)
                .Length(255);

            mapping.Map(x => x.Md5)
                .Length(255);
        }
    }
}
