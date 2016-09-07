using HitechCraft.DAL.Domain;
using HitechCraft.WebAdmin.Models.User;

namespace HitechCraft.WebAdmin.Mapper.User
{
    using Models;

    public class PlayerSkinToPlayerSkinViewModelMapper : BaseMapper<PlayerSkin, PlayerSkinViewModel>
    {
        public PlayerSkinToPlayerSkinViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<PlayerSkin, PlayerSkinViewModel>()
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image));
        }
    }
}