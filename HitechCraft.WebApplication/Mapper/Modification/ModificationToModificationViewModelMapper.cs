namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;
    using System.Linq;

    public class ModificationToModificationViewModelMapper : BaseMapper<Modification, ModificationViewModel>
    {
        public ModificationToModificationViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Modification, ModificationViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description))
                .ForMember(dst => dst.Version, ext => ext.MapFrom(src => src.Version))
                .ForMember(dst => dst.Servers, ext => ext.MapFrom(src => 
                    src.ServerModifications.Select(s => new ModificationServerViewModel()
                    {
                        Id = s.Server.Id,
                        Name = s.Server.Name
                    })));
        }
    }
}