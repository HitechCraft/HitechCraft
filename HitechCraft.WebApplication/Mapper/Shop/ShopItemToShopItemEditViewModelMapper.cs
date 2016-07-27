namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;

    public class ShopItemToShopItemEditViewModelMapper : BaseMapper<ShopItem, ShopItemEditViewModel>
    {
        public ShopItemToShopItemEditViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<ShopItem, ShopItemEditViewModel>()
                .ForMember(dst => dst.GameId, ext => ext.MapFrom(src => src.GameId))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description))
                .ForMember(dst => dst.Price, ext => ext.MapFrom(src => src.Price))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.ModificationId, ext => ext.MapFrom(src => src.Modification.Id))
                .ForMember(dst => dst.CategoryId, ext => ext.MapFrom(src => src.ItemCategory.Id));

        }
    }
}