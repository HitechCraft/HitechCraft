namespace HitechCraft.WebApplication.Mapper
{
    using Models;
    using BL.CQRS.Command;

    public class NewsEditViewModelToNewsUpdateCommandMapper : BaseMapper<NewsEditViewModel, NewsUpdateCommand>
    {
        public NewsEditViewModelToNewsUpdateCommandMapper()
        {
            this.ConfigurationStore.CreateMap<NewsEditViewModel, NewsUpdateCommand>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Title, ext => ext.MapFrom(src => src.Title))
                .ForMember(dst => dst.Text, ext => ext.MapFrom(src => src.Text))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.PlayerName, ext => ext.MapFrom(src => src.AuthorName));
        }
    }
}