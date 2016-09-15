using HitechCraft.Core.Entity;

namespace HitechCraft.WebApplication.Mapper
{
    using GameLauncherAPI.Mapper;
    using GameLauncherAPI.Models;

    public class PlayerSkinToPlayerSkinModelMapper : BaseMapper<PlayerSkin, PlayerSkinModel>
    {
        public PlayerSkinToPlayerSkinModelMapper()
        {
            this.ConfigurationStore.CreateMap<PlayerSkin, PlayerSkinModel>()
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image));
        }
    }
}