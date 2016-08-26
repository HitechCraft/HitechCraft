namespace HitechCraft.WebApplication.Mapper
{
    using Models;
    using BL.CQRS.Command;

    public class PlayerSessionEditViewModelToPlayerSessionUpdateCommandMapper : BaseMapper<PlayerSessionEditViewModel, PlayerSessionUpdateCommand>
    {
        public PlayerSessionEditViewModelToPlayerSessionUpdateCommandMapper()
        {
            this.ConfigurationStore.CreateMap<PlayerSessionEditViewModel, PlayerSessionUpdateCommand>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Server, ext => ext.MapFrom(src => src.Server))
                .ForMember(dst => dst.Session, ext => ext.MapFrom(src => src.Session))
                .ForMember(dst => dst.Token, ext => ext.MapFrom(src => src.Token));
        }
    }
}