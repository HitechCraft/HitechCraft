namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;

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