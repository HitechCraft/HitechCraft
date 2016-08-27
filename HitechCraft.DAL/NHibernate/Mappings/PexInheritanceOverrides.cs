namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion
    
    public class PexInheritanceOverrides : IAutoMappingOverride<PexInheritance>
    {
        public void Override(AutoMapping<PexInheritance> mapping)
        {
            mapping.Table("PexInheritance");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.Map(x => x.Child)
                .Column("child");

            mapping.Map(x => x.Parent)
                .Column("parent");

            mapping.Map(x => x.Type)
                .Column("type");

            mapping.Map(x => x.World)
                .Column("world");
        }
    }
}
