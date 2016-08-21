namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using GameLauncherAPI.Mapper;
    using GameLauncherAPI.Models;

    public class PlayerSkinToPlayerSkinModelMapper : BaseMapper<Skin, PlayerSkinModel>
    {
        public PlayerSkinToPlayerSkinModelMapper()
        {
            this.ConfigurationStore.CreateMap<Skin, PlayerSkinModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image));
        }
    }
}