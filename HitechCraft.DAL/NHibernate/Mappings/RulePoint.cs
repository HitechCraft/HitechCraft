namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using Common.Models.Enum;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class RulePointOverrides : IAutoMappingOverride<RulePoint>
    {
        public void Override(AutoMapping<RulePoint> mapping)
        {
            mapping.Table("RulePoint");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();
            
            mapping.Map(x => x.Name)
                .Column("Name")
                .Length(128)
                .Not.Nullable();
        }
    }
}
