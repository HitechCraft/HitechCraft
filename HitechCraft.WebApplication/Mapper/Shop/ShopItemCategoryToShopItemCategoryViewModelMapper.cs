using HitechCraft.Core.Entity;

namespace HitechCraft.WebApplication.Mapper
{
    using Models;

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