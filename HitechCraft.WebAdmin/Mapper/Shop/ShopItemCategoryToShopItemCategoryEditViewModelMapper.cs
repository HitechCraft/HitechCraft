using HitechCraft.DAL.Domain;
using HitechCraft.WebAdmin.Models;

namespace HitechCraft.WebAdmin.Mapper
{
    public class ShopItemCategoryToShopItemCategoryEditViewModelMapper : BaseMapper<ShopItemCategory, ShopItemCategoryEditViewModel>
    {
        public ShopItemCategoryToShopItemCategoryEditViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<ShopItemCategory, ShopItemCategoryEditViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description));

        }
    }
}