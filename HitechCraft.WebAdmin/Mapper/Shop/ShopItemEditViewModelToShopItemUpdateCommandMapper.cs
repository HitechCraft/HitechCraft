using HitechCraft.BL.CQRS.Command;
using HitechCraft.WebAdmin.Models;

namespace HitechCraft.WebAdmin.Mapper
{
    public class ShopItemEditViewModelToShopItemUpdateCommandMapper : BaseMapper<ShopItemEditViewModel, ShopItemUpdateCommand>
    {
        public ShopItemEditViewModelToShopItemUpdateCommandMapper()
        {
            this.ConfigurationStore.CreateMap<ShopItemEditViewModel, ShopItemUpdateCommand>()
                .ForMember(dst => dst.GameId, ext => ext.MapFrom(src => src.GameId))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Price, ext => ext.MapFrom(src => src.Price))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.ModificationId, ext => ext.MapFrom(src => src.ModificationId))
                .ForMember(dst => dst.CategoryId, ext => ext.MapFrom(src => src.CategoryId));

        }
    }
}