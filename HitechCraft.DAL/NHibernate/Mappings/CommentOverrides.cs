namespace HitechCraft.DAL.NHibernate.Mappings
{
    #region Using Directives
    
    using Domain;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    #endregion

    public class CommentOverrides : IAutoMappingOverride<Comment>
    {
        public void Override(AutoMapping<Comment> mapping)
        {
            mapping.Table("Comment");

            mapping.Id(x => x.Id)
                .GeneratedBy.Increment();

            mapping.Map(x => x.Text)
                .Length(255)
                .Not.Nullable();

            mapping.Map(x => x.TimeCreate);

            mapping.References(x => x.Author)
                .Column("Author");

            mapping.References(x => x.News)
                .Column("News");
        }
    }
}
