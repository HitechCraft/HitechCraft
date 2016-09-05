namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using Common.Models.Enum;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class PlayerOverrides : IAutoMappingOverride<Player>
    {
        public void Override(AutoMapping<Player> mapping)
        {
            mapping.Table("Player");

            mapping.Map(x => x.Id)
                .Generated.Insert();

            mapping.Id(x => x.Name)
                .Column("Name")
                .Length(128)
                .Not.Nullable();
            
            mapping.Map(x => x.Gender)
                .CustomType<Gender>();

            mapping.References(x => x.Info)
                .Column("PlayerInfo")
                .Cascade.All();
        }
    }
}
