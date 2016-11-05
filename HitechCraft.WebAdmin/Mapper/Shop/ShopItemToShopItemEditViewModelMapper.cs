﻿using HitechCraft.Core.Entity;
using HitechCraft.WebAdmin.Models;

namespace HitechCraft.WebAdmin.Mapper
{
    public class ShopItemToShopItemEditViewModelMapper : BaseMapper<ShopItem, ShopItemEditViewModel>
    {
        public ShopItemToShopItemEditViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<ShopItem, ShopItemEditViewModel>()
                .ForMember(dst => dst.GameId, ext => ext.MapFrom(src => src.GameId))
                .ForMember(dst => dst.Price, ext => ext.MapFrom(src => src.Price))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.ModificationId, ext => ext.MapFrom(src => src.Modification.Id))
                .ForMember(dst => dst.CategoryId, ext => ext.MapFrom(src => src.ItemCategory.Id));

        }
    }
}