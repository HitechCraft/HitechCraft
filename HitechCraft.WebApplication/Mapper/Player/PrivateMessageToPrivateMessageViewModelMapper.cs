using System;

namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;
    using System.Linq;
    using Common.Models.Enum;

    public class PrivateMessageToPrivateMessageViewModelMapper : BaseMapper<PrivateMessage, PrivateMessageViewModel>
    {
        public PrivateMessageToPrivateMessageViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<PrivateMessage, PrivateMessageViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Players, ext => ext.MapFrom(src => src.PmPlayerBox))
                .ForMember(dst => dst.Title, ext => ext.MapFrom(src => src.Title))
                .ForMember(dst => dst.Text, ext => ext.MapFrom(src => src.Text))
                .ForMember(dst => dst.TimeCreate, ext => ext.MapFrom(src => src.TimeCreate));

            this.ConfigurationStore.CreateMap<PMPlayerBox, PMPlayerViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.PlayerName, ext => ext.MapFrom(src => src.Player.Name))
                .ForMember(dst => dst.MessageType, ext => ext.MapFrom(src => src.PmType))
                .ForMember(dst => dst.PlayerType, ext => ext.MapFrom(src => src.PlayerType));
        }
    }
}