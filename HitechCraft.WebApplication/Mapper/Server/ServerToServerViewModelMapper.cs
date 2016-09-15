using HitechCraft.Core.Entity;
using HitechCraft.Core.Entity.Extentions;

namespace HitechCraft.WebApplication.Mapper
{
    using Models;

    public class ServerToServerViewModelMapper : BaseMapper<Server, ServerViewModel>
    {
        public ServerToServerViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Server, ServerViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.ClientVersion, ext => ext.MapFrom(src => src.ClientVersion))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.Address, ext => ext.MapFrom(src => src.IpAddress + ":" + src.Port))
                .ForMember(dst => dst.Data, ext => ext.MapFrom(src => src.GetServerData(src.Image)));
        }
    }
}