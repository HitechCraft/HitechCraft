using HitechCraft.DAL.Domain.Extentions;
using HitechCraft.WebAdmin.Models;

namespace HitechCraft.WebAdmin.Mapper
{
    public class ServerToServerViewModelMapper : BaseMapper<DAL.Domain.Server, ServerViewModel>
    {
        public ServerToServerViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<DAL.Domain.Server, ServerViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.ClientVersion, ext => ext.MapFrom(src => src.ClientVersion))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.IpAddress, ext => ext.MapFrom(src => src.IpAddress))
                .ForMember(dst => dst.ServerPort, ext => ext.MapFrom(src => src.Port))
                .ForMember(dst => dst.MapPort, ext => ext.MapFrom(src => src.MapPort))
                .ForMember(dst => dst.Data, ext => ext.MapFrom(src => src.GetServerData(src.Image)));
        }
    }
}