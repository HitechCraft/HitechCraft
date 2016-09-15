namespace HitechCraft.WebApplication.Mapper
{
    using Models;
    using BL.CQRS.Command;

    public class ShopItemEditViewModelToShopItemCreateCommandMapper : BaseMapper<ShopItemEditViewModel, ShopItemCreateCommand>
    {
        public ShopItemEditViewModelToShopItemCreateCommandMapper()
        {
            this.ConfigurationStore.CreateMap<ShopItemEditViewModel, ShopItemCreateCommand>()
                .ForMember(dst => dst.GameId, ext => ext.MapFrom(src => src.GameId))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description))
                .ForMember(dst => dst.Price, ext => ext.MapFrom(src => src.Price))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.ModificationId, ext => ext.MapFrom(src => src.ModificationId))
                .ForMember(dst => dst.CategoryId, ext => ext.MapFrom(src => src.CategoryId));

        }
    }
}