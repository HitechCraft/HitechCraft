using HitechCraft.Core.Entity;

namespace HitechCraft.WebApplication.Mapper
{
    using Models;

    public class PlayerToPlayerProfileViewModelMapper : BaseMapper<Player, PlayerProfileViewModel>
    {
        public PlayerToPlayerProfileViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Player, PlayerProfileViewModel>()
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Email, ext => ext.MapFrom(src => src.Info != null ? src.Info.Email : ""))
                .ForMember(dst => dst.Gender, ext => ext.MapFrom(src => src.Gender));
        }
    }
}