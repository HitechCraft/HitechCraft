namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;

    public class NewsToNewsViewModelMapper : BaseMapper<News, NewsViewModel>
    {
        public NewsToNewsViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<News, NewsViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.AuthorName, ext => ext.MapFrom(src => src.Author.Name))
                .ForMember(dst => dst.ShortText, ext => ext.MapFrom(src => src.Text.Substring(0, 500) + "..."))
                .ForMember(dst => dst.FullText, ext => ext.MapFrom(src => src.Text))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.Title, ext => ext.MapFrom(src => src.Title))
                .ForMember(dst => dst.TimeCreate, ext => ext.MapFrom(src => src.TimeCreate))
                .ForMember(dst => dst.ViewersCount, ext => ext.MapFrom(src => src.ViewersCount));
        }
    }
}