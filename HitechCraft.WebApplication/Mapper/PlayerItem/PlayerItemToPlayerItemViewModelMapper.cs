﻿namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;

    public class PlayerItemToPlayerItemViewModelMapper : BaseMapper<PlayerItem, PlayerItemViewModel>
    {
        public PlayerItemToPlayerItemViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<PlayerItem, PlayerItemViewModel>()
                .ForMember(dst => dst.ItemGameId, ext => ext.MapFrom(src => src.Item.GameId))
                .ForMember(dst => dst.ItemName, ext => ext.MapFrom(src => src.Item.Name))
                .ForMember(dst => dst.ItemDescription, ext => ext.MapFrom(src => src.Item.Description))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Item.Image))
                .ForMember(dst => dst.Count, ext => ext.MapFrom(src => src.Count));
        }
    }
}