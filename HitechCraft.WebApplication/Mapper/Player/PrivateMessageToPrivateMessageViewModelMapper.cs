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
                .ForMember(dst => dst.AuthorName, ext => ext.MapFrom(src => src.PmPlayerBox.FirstOrDefault(x => x.PlayerType == PMPlayerType.Author).Player.Name))
                .ForMember(dst => dst.RecipientName, ext => ext.MapFrom(src => src.PmPlayerBox.FirstOrDefault(x => x.PlayerType == PMPlayerType.Recipient).Player.Name))
                .ForMember(dst => dst.Title, ext => ext.MapFrom(src => src.Title))
                .ForMember(dst => dst.Text, ext => ext.MapFrom(src => src.Text))
                .ForMember(dst => dst.TimeCreate, ext => ext.MapFrom(src => src.TimeCreate));
        }
    }
}