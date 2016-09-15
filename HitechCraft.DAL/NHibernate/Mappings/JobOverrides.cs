namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives

    using HitechCraft.Core.Entity;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class JobOverrides : IAutoMappingOverride<Job>
    {
        public void Override(AutoMapping<Job> mapping)
        {
            mapping.Table("Jobsjobs");

            mapping.Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Increment();
            
            mapping.Map(x => x.Uuid)
                .Column("player_uuid")
                .Not.Nullable();

            mapping.Map(x => x.PlayerName)
                .Column("username")
                .Length(128)
                .Not.Nullable();

            mapping.Map(x => x.JobName)
                .Column("job")
                .Length(128)
                .Not.Nullable();

            mapping.Map(x => x.Experiance)
                .Column("experience")
                .Length(11)
                .Not.Nullable();

            mapping.Map(x => x.Level)
                .Column("level")
                .Length(11)
                .Not.Nullable();
        }
    }
}
