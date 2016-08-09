namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;

    public class PermissionsToPermissionsViewModelMapper : BaseMapper<Permissions, PermissionsViewModel>
    {
        public PermissionsToPermissionsViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Permissions, PermissionsViewModel>()
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Permission, ext => ext.MapFrom(src => src.Permission))
                .ForMember(dst => dst.Value, ext => ext.MapFrom(src => src.Value));
        }
    }
}