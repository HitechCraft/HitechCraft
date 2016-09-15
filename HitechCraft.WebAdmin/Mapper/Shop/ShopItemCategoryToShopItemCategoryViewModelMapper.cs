using HitechCraft.Core.Entity;
using HitechCraft.WebAdmin.Models;

namespace HitechCraft.WebAdmin.Mapper
{
    public class ShopItemCategoryToShopItemCategoryViewModelMapper : BaseMapper<ShopItemCategory, ShopItemCategoryViewModel>
    {
        public ShopItemCategoryToShopItemCategoryViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<ShopItemCategory, ShopItemCategoryViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description));

        }
    }
}