using HitechCraft.Core.Entity;

namespace HitechCraft.WebApplication.Mapper
{
    using Models;

    public class CommentToCommentEditViewModelMapper : BaseMapper<Comment, CommentEditViewModel>
    {
        public CommentToCommentEditViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Comment, CommentEditViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Text, ext => ext.MapFrom(src => src.Text));

            this.ConfigurationStore.CreateMap<CommentEditViewModel, Comment>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Text, ext => ext.MapFrom(src => src.Text));
        }
    }
}