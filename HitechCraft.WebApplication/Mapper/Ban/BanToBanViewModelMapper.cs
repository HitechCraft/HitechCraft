namespace HitechCraft.WebApplication.Mapper
{
    #region Using Directives

    using DAL.Domain;
    using Models;
    using System;
    using Common.Models.Enum;

    #endregion

    public class BanToBanViewModelMapper : BaseMapper<Ban, BanViewModel>
    {
        public BanToBanViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Ban, BanViewModel>()
                .ForMember(dst => dst.Id, exp => exp.MapFrom(src => src.Id))
                .ForMember(dst => dst.PlayerName, exp => exp.MapFrom(src => src.Player.Name))
                .ForMember(dst => dst.Reason, exp => exp.MapFrom(src => src.Player.Name))
                .ForMember(dst => dst.BannedBy, exp => exp.MapFrom(src => src.Admin.Name))
                .ForMember(dst => dst.ActionTime, exp => exp.MapFrom(src =>
                    new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(src.Time).ToLocalTime()))
                .ForMember(dst => dst.TempTime, exp => exp.MapFrom(src =>
                    new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(src.TempTime).ToLocalTime()))
                .ForMember(dst => dst.Type, exp => exp.MapFrom(src => (BanType)src.Type));
        }
    }
}