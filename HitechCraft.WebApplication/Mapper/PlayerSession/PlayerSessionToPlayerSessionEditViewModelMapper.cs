using HitechCraft.Core.Entity;

namespace HitechCraft.WebApplication.Mapper
{
    using Models;

    public class PlayerSessionToPlayerSessionEditViewModelMapper : BaseMapper<PlayerSession, PlayerSessionEditViewModel>
    {
        public PlayerSessionToPlayerSessionEditViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<PlayerSession, PlayerSessionEditViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.PlayerName, ext => ext.MapFrom(src => src.Player.Name))
                .ForMember(dst => dst.Server, ext => ext.MapFrom(src => src.Server))
                .ForMember(dst => dst.Session, ext => ext.MapFrom(src => src.Session))
                .ForMember(dst => dst.Md5, ext => ext.MapFrom(src => src.Md5))
                .ForMember(dst => dst.Token, ext => ext.MapFrom(src => src.Token));

        }
    }
}