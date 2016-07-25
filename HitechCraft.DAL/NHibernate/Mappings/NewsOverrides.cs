namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class NewsOverrides : IAutoMappingOverride<News>
    {
        public void Override(AutoMapping<News> mapping)
        {
            mapping.Table("News");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.Map(x => x.Title)
                .Length(128)
                .Not.Nullable();

            mapping.Map(x => x.Text)
                .Length(2000)
                .Not.Nullable();

            mapping.Map(x => x.TimeCreate);

            mapping.Map(x => x.Image);

            mapping.Map(x => x.ViewersCount);

            mapping.References(x => x.Author, "Author");

            mapping.HasMany(x => x.Comments);
        }
    }
}
