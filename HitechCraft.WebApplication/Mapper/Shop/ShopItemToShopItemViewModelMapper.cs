using HitechCraft.Core.Entity;

namespace HitechCraft.WebApplication.Mapper
{
    using Models;

    public class ShopItemToShopItemViewModelMapper : BaseMapper<ShopItem, ShopItemViewModel>
    {
        public ShopItemToShopItemViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<ShopItem, ShopItemViewModel>()
                .ForMember(dst => dst.GameId, ext => ext.MapFrom(src => src.GameId))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Price, ext => ext.MapFrom(src => src.Price))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.ModificationId, ext => ext.MapFrom(src => src.Modification.Id))
                .ForMember(dst => dst.ModificationName, ext => ext.MapFrom(src => src.Modification.Name))
                .ForMember(dst => dst.CategoryId, ext => ext.MapFrom(src => src.ItemCategory.Id))
                .ForMember(dst => dst.CategoryName, ext => ext.MapFrom(src => src.ItemCategory.Name));

        }
    }
}