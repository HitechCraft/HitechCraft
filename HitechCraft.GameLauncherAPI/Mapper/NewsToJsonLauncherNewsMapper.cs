using HitechCraft.Core.Entity;
using HitechCraft.Core.Entity.Extentions;
using HitechCraft.Core.Models.Json;

namespace HitechCraft.GameLauncherAPI.Mapper
{
    public class NewsToJsonLauncherNewsMapper : BaseMapper<News, JsonLauncherNews>
    {
        public NewsToJsonLauncherNewsMapper()
        {
            this.ConfigurationStore.CreateMap<News, JsonLauncherNews>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Title, ext => ext.MapFrom(src => src.Title))
                .ForMember(dst => dst.Text, ext => ext.MapFrom(src => src.Text.Limit(215) + "..."));
        }
    }
}