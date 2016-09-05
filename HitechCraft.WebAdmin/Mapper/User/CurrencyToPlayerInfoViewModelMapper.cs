using System;
using HitechCraft.Common.Models.Enum;
using HitechCraft.DAL.Domain;

namespace HitechCraft.WebAdmin.Mapper.User
{
    using Models;

    public class CurrencyToPlayerInfoViewModelMapper : BaseMapper<Currency, PlayerInfoViewModel>
    {
        public CurrencyToPlayerInfoViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Currency, PlayerInfoViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Player != null ? src.Player.Name : String.Empty))
                .ForMember(dst => dst.Gender, ext => ext.MapFrom(src => src.Player != null ? src.Player.Gender : Gender.Male))
                .ForMember(dst => dst.Gonts, ext => ext.MapFrom(src => src.Gonts))
                .ForMember(dst => dst.Rubles, ext => ext.MapFrom(src => src.Rubels));
        }
    }
}