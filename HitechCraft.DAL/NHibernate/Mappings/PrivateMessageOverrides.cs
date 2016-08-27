namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using Common.Models.Enum;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class PrivateMessageOverrides : IAutoMappingOverride<PrivateMessage>
    {
        public void Override(AutoMapping<PrivateMessage> mapping)
        {
            mapping.Table("PrivateMessage");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();
            
            mapping.Map(x => x.Title)
                .Column("Title")
                .Length(128)
                .Not.Nullable();

            mapping.Map(x => x.Text)
                .Column("Text")
                .Length(2000)
                .Not.Nullable();

            mapping.Map(x => x.TimeCreate)
                .Column("TimeCreate")
                .Not.Nullable();

            mapping.HasMany(x => x.PmPlayerBox);
        }
    }
}
