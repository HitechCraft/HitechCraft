namespace HitechCraft.WebAdmin.Mapper.User
{
    using Models;

    public class UserToUserViewModelMapper : BaseMapper<ApplicationUser, UserViewModel>
    {
        public UserToUserViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.UserName))
                .ForMember(dst => dst.Email, ext => ext.MapFrom(src => src.Email));
        }
    }
}