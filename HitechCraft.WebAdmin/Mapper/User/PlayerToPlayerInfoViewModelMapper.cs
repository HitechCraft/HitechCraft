using HitechCraft.DAL.Domain;

namespace HitechCraft.WebAdmin.Mapper.User
{
    using Models;

    public class PlayerToPlayerInfoViewModelMapper : BaseMapper<Player, PlayerInfoViewModel>
    {
        public PlayerToPlayerInfoViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Player, PlayerInfoViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Gender, ext => ext.MapFrom(src => src.Gender))
                .ForMember(dst => dst.Email, ext => ext.MapFrom(src => src.Info != null ? src.Info.Email : "no"))
                .ForMember(dst => dst.Gonts, ext => ext.MapFrom(src => src.Currency != null ? src.Currency.Gonts : 0))
                .ForMember(dst => dst.Rubles, ext => ext.MapFrom(src => src.Currency != null ? src.Currency.Rubels : 0));
        }
    }
}