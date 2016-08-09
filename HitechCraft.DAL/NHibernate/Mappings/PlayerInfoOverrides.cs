namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using Common.Models.Enum;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class PlayerInfoOverrides : IAutoMappingOverride<PlayerInfo>
    {
        public void Override(AutoMapping<PlayerInfo> mapping)
        {
            mapping.Table("PlayerInfo");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.Map(x => x.Email)
                .Column("Email")
                .Length(128)
                .Not.Nullable();

            mapping.References(x => x.Refer)
                .Column("Refer")
                .Nullable();
        }
    }
}
