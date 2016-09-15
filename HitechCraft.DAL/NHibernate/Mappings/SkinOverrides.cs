namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives

    using HitechCraft.Core.Entity;
    using HitechCraft.Core.Models.Enum;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class SkinOverrides : IAutoMappingOverride<Skin>
    {
        public void Override(AutoMapping<Skin> mapping)
        {
            mapping.Table("Skin");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();
            
            mapping.Map(x => x.Name)
                .Column("Name")
                .Length(128)
                .Not.Nullable();

            mapping.Map(x => x.Image)
                .Column("Image")
                .Not.Nullable();

            mapping.Map(x => x.Gender)
                .Column("Gender")
                .CustomType<Gender>();
        }
    }
}
