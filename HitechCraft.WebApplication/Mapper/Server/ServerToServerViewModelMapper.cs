namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;
    using System.Linq;
    using DAL.Domain.Extentions;

    public class ServerToServerViewModelMapper : BaseMapper<Server, ServerViewModel>
    {
        public ServerToServerViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Server, ServerViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.ClientVersion, ext => ext.MapFrom(src => src.ClientVersion))
                .ForMember(dst => dst.IpAddress, ext => ext.MapFrom(src => src.IpAddress))
                .ForMember(dst => dst.Port, ext => ext.MapFrom(src => src.Port))
                .ForMember(dst => dst.MapPort, ext => ext.MapFrom(src => src.MapPort))
                .ForMember(dst => dst.Data, ext => ext.MapFrom(src => src.GetServerData()))
                .ForMember(dst => dst.Modifications, ext => ext.MapFrom(src => src.ServerModifications.Select(x => x.Modification)));

            this.ConfigurationStore.CreateMap<Modification, ServerModificationViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name));

        }
    }
}