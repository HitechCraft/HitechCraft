namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives

    using HitechCraft.Core.Entity;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class IKTransactionOverrides : IAutoMappingOverride<IKTransaction>
    {
        public void Override(AutoMapping<IKTransaction> mapping)
        {
            mapping.Table("IKTransaction");

            mapping.Map(x => x.Id)
                .Generated.Insert();

            mapping.Id(x => x.TransactionId)
                .Length(128);

            mapping.References(x => x.Player)
                .Column("Player");

            mapping.Map(x => x.Time);
        }
    }
}
