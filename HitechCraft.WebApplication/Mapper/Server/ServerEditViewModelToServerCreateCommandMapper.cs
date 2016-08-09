namespace HitechCraft.WebApplication.Mapper
{
    using Models;
    using BL.CQRS.Command;

    public class ServerEditViewModelToServerCreateCommandMapper : BaseMapper<ServerEditViewModel, ServerCreateCommand>
    {
        public ServerEditViewModelToServerCreateCommandMapper()
        {
            this.ConfigurationStore.CreateMap<ServerEditViewModel, ServerCreateCommand>()
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.ClientVersion, ext => ext.MapFrom(src => src.ClientVersion))
                .ForMember(dst => dst.IpAddress, ext => ext.MapFrom(src => src.IpAddress))
                .ForMember(dst => dst.Port, ext => ext.MapFrom(src => src.Port))
                .ForMember(dst => dst.MapPort, ext => ext.MapFrom(src => src.MapPort));

        }
    }
}