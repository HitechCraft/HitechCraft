using HitechCraft.Core.Entity;

namespace HitechCraft.WebApplication.Mapper
{
    #region Using Directives

    using Models;

    #endregion

    public class AuthLogToAuthLogViewModelMapper : BaseMapper<AuthLog, AuthLogViewModel>
    {
        public AuthLogToAuthLogViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<AuthLog, AuthLogViewModel>()
                .ForMember(dst => dst.Id, exp => exp.MapFrom(src => src.Id))
                .ForMember(dst => dst.Ip, exp => exp.MapFrom(src => src.Ip))
                .ForMember(dst => dst.Browser, exp => exp.MapFrom(src => src.Browser))
                .ForMember(dst => dst.Time, exp => exp.MapFrom(src => src.Time));
        }
    }
}