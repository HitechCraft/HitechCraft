namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class ModificationOverrides : IAutoMappingOverride<Modification>
    {
        public void Override(AutoMapping<Modification> mapping)
        {
            mapping.Table("Modification");

            mapping.Id(x => x.Id)
                .Column("Id")
                .GeneratedBy.Increment();

            mapping.Map(x => x.Name)
                .Column("Name")
                .Length(128)
                .Not.Nullable();

            mapping.Map(x => x.Description)
                .Column("Description")
                .Length(2000);

            mapping.Map(x => x.Image)
                .Column("Image");

            mapping.Map(x => x.GuideVideo)
                .Column("GuideVideo")
                .Length(128)
                .Nullable();

            mapping.Map(x => x.Version)
                .Column("Version")
                .Length(32)
                .Not.Nullable();

            mapping.HasMany(x => x.ServerModifications);
        }
    }
}
