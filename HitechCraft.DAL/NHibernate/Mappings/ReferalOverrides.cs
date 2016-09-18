namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives

    using HitechCraft.Core.Entity;
    using HitechCraft.Core.Models.Enum;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class ReferalOverrides : IAutoMappingOverride<Referal>
    {
        public void Override(AutoMapping<Referal> mapping)
        {
            mapping.Table("Referal");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();
            
            mapping.References(x => x.Refer)
                .Column("Refer")
                .Cascade.All();

            mapping.References(x => x.Referer)
                .Column("Referer")
                .Cascade.All();
        }
    }
}
