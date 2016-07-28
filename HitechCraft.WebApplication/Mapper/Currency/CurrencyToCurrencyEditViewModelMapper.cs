﻿namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;

    public class CurrencyToCurrencyEditViewModelMapper : BaseMapper<Currency, CurrencyEditViewModel>
    {
        public CurrencyToCurrencyEditViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Currency, CurrencyEditViewModel>()
                .ForMember(dst => dst.PlayerId, ext => ext.MapFrom(src => src.Player.Id))
                .ForMember(dst => dst.PlayerName, ext => ext.MapFrom(src => src.Player.Name))
                .ForMember(dst => dst.Gonts, ext => ext.MapFrom(src => src.Gonts))
                .ForMember(dst => dst.Rubles, ext => ext.MapFrom(src => src.Rubels));
        }
    }
}