namespace HitechCraft.WebApplication.Mapper
{
    using Models;
    using BL.CQRS.Command;

    public class ShopItemCategoryEditViewModelToShopItemCategoryCreateCommandMapper : BaseMapper<ShopItemCategoryEditViewModel, ShopItemCategoryCreateCommand>
    {
        public ShopItemCategoryEditViewModelToShopItemCategoryCreateCommandMapper()
        {
            this.ConfigurationStore.CreateMap<ShopItemCategoryEditViewModel, ShopItemCategoryCreateCommand>()
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description));

        }
    }
}