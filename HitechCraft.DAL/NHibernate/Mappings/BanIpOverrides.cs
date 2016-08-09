﻿namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class BanIpOverrides : IAutoMappingOverride<BanIp>
    {
        public void Override(AutoMapping<BanIp> mapping)
        {
            mapping.Table("BanIp");

            mapping.Id(x => x.Id)
                .Column("id")
                .GeneratedBy.Increment();

            mapping.References(x => x.Player)
                .Column("name")
                .Not.Nullable();

            mapping.Map(x => x.LastIp)
                .Column("lastip");
        }
    }
}
