namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives

    using HitechCraft.Core.Entity;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class ServerModificationOverrides : IAutoMappingOverride<ServerModification>
    {
        public void Override(AutoMapping<ServerModification> mapping)
        {
            mapping.Table("ServerModification");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.References(x => x.Modification)
                .Column("Modification");

            mapping.References(x => x.Server)
                .Column("Server");
        }
    }
}
