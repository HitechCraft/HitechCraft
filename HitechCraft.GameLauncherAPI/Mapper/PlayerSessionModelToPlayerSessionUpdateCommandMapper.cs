namespace HitechCraft.GameLauncherAPI.Mapper
{
    using BL.CQRS.Command;
    using Models;

    public class PlayerSessionModelToPlayerSessionUpdateCommandMapper : BaseMapper<PlayerSessionModel, PlayerSessionUpdateCommand>
    {
        public PlayerSessionModelToPlayerSessionUpdateCommandMapper()
        {
            this.ConfigurationStore.CreateMap<PlayerSessionModel, PlayerSessionUpdateCommand>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Server, ext => ext.MapFrom(src => src.Server))
                .ForMember(dst => dst.Session, ext => ext.MapFrom(src => src.Session))
                .ForMember(dst => dst.Token, ext => ext.MapFrom(src => src.Token));
        }
    }
}