namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives

    using HitechCraft.Core.Entity;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class RuleOverrides : IAutoMappingOverride<Rule>
    {
        public void Override(AutoMapping<Rule> mapping)
        {
            mapping.Table("Rule");

            mapping.Id(x => x.Id)
                .Column("Id")
                .GeneratedBy.Increment();

            mapping.Map(x => x.Text)
                .Column("Text");

            mapping.References(x => x.Point)
                .Column("Point")
                .Not.Nullable();
        }
    }
}
