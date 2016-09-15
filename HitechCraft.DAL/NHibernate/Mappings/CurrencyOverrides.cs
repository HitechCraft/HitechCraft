namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives

    using HitechCraft.Core.Entity;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class CurrencyOverrides : IAutoMappingOverride<Currency>
    {
        public void Override(AutoMapping<Currency> mapping)
        {
            mapping.Table("Currency");

            mapping.Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Increment();

            mapping.Map(x => x.Gonts)
                .Column("balance");

            mapping.Map(x => x.Rubels)
                .Column("realmoney");

            mapping.Map(x => x.Status)
                .Column("status");

            mapping.References(x => x.Player)
                .Column("username")
                .Unique();
        }
    }
}
