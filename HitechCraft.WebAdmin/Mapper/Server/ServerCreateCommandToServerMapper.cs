﻿using HitechCraft.BL.CQRS.Command;

namespace HitechCraft.WebAdmin.Mapper
{
    public class ServerCreateCommandToServerMapper : BaseMapper<ServerCreateCommand, DAL.Domain.Server>
    {
        public ServerCreateCommandToServerMapper()
        {
            this.ConfigurationStore.CreateMap<ServerCreateCommand, DAL.Domain.Server>()
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