namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using FluentNHibernate.Automapping.Alterations;
    using FluentNHibernate.Automapping;

    #endregion

    public class BanOverrides : IAutoMappingOverride<Ban>
    {
        public void Override(AutoMapping<Ban> mapping)
        {
            mapping.Table("Ban");

            mapping.Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Increment();

            mapping.References(x => x.Player)
                .Column("name")
                .Not.Nullable();

            mapping.Map(x => x.Reason)
                .Column("reason")
                .Not.Nullable();

            mapping.References(x => x.Admin)
                .Column("admin")
                .Not.Nullable();

            mapping.Map(x => x.Time)
                .Column("time")
                .Length(32);

            mapping.Map(x => x.TempTime)
                .Column("temptime")
                .Length(32);

            mapping.Map(x => x.Type)
                .Column("type")
                .Length(11);
        }
    }
}
