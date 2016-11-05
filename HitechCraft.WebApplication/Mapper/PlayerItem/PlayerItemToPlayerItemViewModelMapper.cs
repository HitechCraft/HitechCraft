using HitechCraft.Core.Entity;

namespace HitechCraft.WebApplication.Mapper
{
    using Models;

    public class PlayerItemToPlayerItemViewModelMapper : BaseMapper<PlayerItem, PlayerItemViewModel>
    {
        public PlayerItemToPlayerItemViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<PlayerItem, PlayerItemViewModel>()
                .ForMember(dst => dst.ItemGameId, ext => ext.MapFrom(src => src.Item.GameId))
                .ForMember(dst => dst.ItemName, ext => ext.MapFrom(src => src.Item.Name))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Item.Image))
                .ForMember(dst => dst.Count, ext => ext.MapFrom(src => src.Count));
        }
    }
}