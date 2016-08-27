namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;

    public class CurrencyToCurrencyTopViewModelMapper : BaseMapper<Currency, CurrencyTopViewModel>
    {
        public CurrencyToCurrencyTopViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Currency, CurrencyTopViewModel>()
                .ForMember(dst => dst.PlayerId, ext => ext.MapFrom(src => src.Player.Id))
                .ForMember(dst => dst.PlayerName, ext => ext.MapFrom(src => src.Player.Name))
                .ForMember(dst => dst.PlayerGender, ext => ext.MapFrom(src => src.Player.Gender))
                .ForMember(dst => dst.Gonts, ext => ext.MapFrom(src => src.Gonts))
                .ForMember(dst => dst.Rubles, ext => ext.MapFrom(src => src.Rubels));
        }
    }
}