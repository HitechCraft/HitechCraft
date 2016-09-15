using HitechCraft.Core.Entity;
using HitechCraft.Core.Models.Json;

namespace HitechCraft.WebApplication.Mapper
{
    public class PlayerSessionToJsonSessionDataMapper : BaseMapper<PlayerSession, JsonSessionData>
    {
        public PlayerSessionToJsonSessionDataMapper()
        {
            this.ConfigurationStore.CreateMap<PlayerSession, JsonSessionData>()
                .ForMember(dst => dst.PlayerName, ext => ext.MapFrom(src => src.Player.Name))
                .ForMember(dst => dst.ServerId, ext => ext.MapFrom(src => src.Server))
                .ForMember(dst => dst.SessionId, ext => ext.MapFrom(src => src.Session))
                .ForMember(dst => dst.Md5, ext => ext.MapFrom(src => src.Md5))
                .ForMember(dst => dst.Token, ext => ext.MapFrom(src => src.Token));
        }
    }
}