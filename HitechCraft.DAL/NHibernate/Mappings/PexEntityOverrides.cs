namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives

    using HitechCraft.Core.Entity;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion
    
    public class PexEntityOverrides : IAutoMappingOverride<PexEntity>
    {
        public void Override(AutoMapping<PexEntity> mapping)
        {
            mapping.Table("PexEntity");

            mapping.Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Increment();

            mapping.Map(x => x.Name)
                .Column("name");

            mapping.Map(x => x.Type)
                .Column("type");

            mapping.Map(x => x.Default)
                .Column("default");
        }
    }
}
