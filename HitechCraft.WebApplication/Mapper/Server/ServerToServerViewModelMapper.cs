namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;
    using DAL.Domain.Extentions;
    using System.Collections.Generic;
    
    public class ServerToServerViewModelMapper : BaseMapper<Server, ServerViewModel>
    {
        public ServerToServerViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Server, ServerViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.Address, ext => ext.MapFrom(src => src.IpAddress + ":" + src.Port))
                .ForMember(dst => dst.Data, ext => ext.MapFrom(src => src.GetServerData()));
        }
    }
}