namespace HitechCraft.WebAdmin.Mapper
{
    using BL.CQRS.Command;
    using Models;

    public class ModificationEditViewModelToModificationUpdateCommandMapper : BaseMapper<ModificationEditViewModel, ModificationUpdateCommand>
    {
        public ModificationEditViewModelToModificationUpdateCommandMapper()
        {
            this.ConfigurationStore.CreateMap<ModificationEditViewModel, ModificationUpdateCommand>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.GuideVideo, ext => ext.MapFrom(src => src.GuideVideoCode))
                .ForMember(dst => dst.Version, ext => ext.MapFrom(src => src.Version));
        }
    }
}