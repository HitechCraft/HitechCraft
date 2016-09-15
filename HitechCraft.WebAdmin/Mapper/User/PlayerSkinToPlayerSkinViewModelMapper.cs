namespace HitechCraft.WebAdmin.Mapper
{
    using Models;
    using Core.Entity;

    public class PlayerSkinToPlayerSkinViewModelMapper : BaseMapper<PlayerSkin, PlayerSkinViewModel>
    {
        public PlayerSkinToPlayerSkinViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<PlayerSkin, PlayerSkinViewModel>()
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image));
        }
    }
}