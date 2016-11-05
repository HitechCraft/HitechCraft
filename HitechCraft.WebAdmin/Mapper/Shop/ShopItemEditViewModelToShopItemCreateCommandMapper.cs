using HitechCraft.BL.CQRS.Command;
using HitechCraft.WebAdmin.Models;

namespace HitechCraft.WebAdmin.Mapper
{
    public class ShopItemEditViewModelToShopItemCreateCommandMapper : BaseMapper<ShopItemEditViewModel, ShopItemCreateCommand>
    {
        public ShopItemEditViewModelToShopItemCreateCommandMapper()
        {
            this.ConfigurationStore.CreateMap<ShopItemEditViewModel, ShopItemCreateCommand>()
                .ForMember(dst => dst.GameId, ext => ext.MapFrom(src => src.GameId))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Price, ext => ext.MapFrom(src => src.Price))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.ModificationId, ext => ext.MapFrom(src => src.ModificationId))
                .ForMember(dst => dst.CategoryId, ext => ext.MapFrom(src => src.CategoryId));

        }
    }
}