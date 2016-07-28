namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;

    public class CommentToCommentViewModelMapper : BaseMapper<Comment, CommentViewModel>
    {
        public CommentToCommentViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Comment, CommentViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Text, ext => ext.MapFrom(src => src.Text))
                .ForMember(dst => dst.AuthorId, ext => ext.MapFrom(src => src.Author.Id))
                .ForMember(dst => dst.AuthorName, ext => ext.MapFrom(src => src.Author.Name))
                .ForMember(dst => dst.NewsId, ext => ext.MapFrom(src => src.News.Id))
                .ForMember(dst => dst.TimeCreate, ext => ext.MapFrom(src => src.TimeCreate));
        }
    }
}