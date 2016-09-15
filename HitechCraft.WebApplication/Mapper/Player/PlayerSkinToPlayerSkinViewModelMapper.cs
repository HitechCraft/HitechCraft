using HitechCraft.Core.Entity;

namespace HitechCraft.WebApplication.Mapper
{
    using Models;

    public class PlayerSkinToPlayerSkinViewModelMapper : BaseMapper<PlayerSkin, PlayerSkinViewModel>
    {
        public PlayerSkinToPlayerSkinViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<PlayerSkin, PlayerSkinViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image));
        }
    }
}