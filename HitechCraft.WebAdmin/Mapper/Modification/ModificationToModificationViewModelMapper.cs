using System.Linq;
using HitechCraft.WebAdmin.Models.Modification;

namespace HitechCraft.WebAdmin.Mapper.Modification
{
    public class ModificationToModificationViewModelMapper : BaseMapper<DAL.Domain.Modification, ModificationViewModel>
    {
        public ModificationToModificationViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<DAL.Domain.Modification, ModificationViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description))
                .ForMember(dst => dst.Version, ext => ext.MapFrom(src => src.Version))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.GuideVideoCode, ext => ext.MapFrom(src => src.GuideVideo))
                .ForMember(dst => dst.Servers, ext => ext.MapFrom(src => 
                    src.ServerModifications.Select(s => new ModificationServerViewModel()
                    {
                        Id = s.Server.Id,
                        Name = s.Server.Name
                    })));
        }
    }
}