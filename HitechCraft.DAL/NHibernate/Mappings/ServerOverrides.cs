namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class ServerOverrides : IAutoMappingOverride<Server>
    {
        public void Override(AutoMapping<Server> mapping)
        {
            mapping.Table("Server");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.Map(x => x.Name)
                .Length(128);

            mapping.Map(x => x.Description)
                .Length(2000);

            mapping.Map(x => x.Image)
                .Nullable();

            mapping.Map(x => x.ClientVersion)
                .Length(32);

            mapping.Map(x => x.IpAddress)
                .Length(16);

            mapping.Map(x => x.Port)
                .Length(5);

            mapping.Map(x => x.MapPort)
                .Length(5);

            mapping.HasMany(x => x.ServerModifications);
        }
    }
}
