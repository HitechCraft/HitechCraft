namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion
    
    public class PermissionsOverrides : IAutoMappingOverride<Permissions>
    {
        public void Override(AutoMapping<Permissions> mapping)
        {
            mapping.Table("Permissions");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.Map(x => x.Name)
                .Column("name");

            mapping.Map(x => x.Permission)
                .Column("permission");

            mapping.Map(x => x.Type)
                .Column("type");

            mapping.Map(x => x.Value)
                .Column("value");

            mapping.Map(x => x.World)
                .Column("world");
        }
    }
}
