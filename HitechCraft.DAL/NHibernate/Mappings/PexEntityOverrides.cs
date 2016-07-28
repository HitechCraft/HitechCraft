﻿namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives

    using FluentNHibernate.Mapping;
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    //TODO: поработать позже с разрешениями
    public class PexEntityOverrides : IAutoMappingOverride<PexEntity>
    {
        public void Override(AutoMapping<PexEntity> mapping)
        {
            mapping.Table("PexEntity");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.Map(x => x.Name)
                .Column("name");

            mapping.Map(x => x.Type)
                .Column("type");

            mapping.Map(x => x.Default)
                .Column("default");
        }
    }
}
