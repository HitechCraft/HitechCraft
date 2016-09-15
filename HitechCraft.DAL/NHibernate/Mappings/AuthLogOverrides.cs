namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives

    using HitechCraft.Core.Entity;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class AuthLogOverrides : IAutoMappingOverride<AuthLog>
    {
        public void Override(AutoMapping<AuthLog> mapping)
        {
            mapping.Table("AuthLog");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();
            
            mapping.Map(x => x.Ip)
                .Column("Ip")
                .Length(128)
                .Not.Nullable();

            mapping.Map(x => x.Browser)
                .Column("Browser")
                .Length(128)
                .Not.Nullable();

            mapping.References(x => x.Player)
                .Column("Player");
        }
    }
}
