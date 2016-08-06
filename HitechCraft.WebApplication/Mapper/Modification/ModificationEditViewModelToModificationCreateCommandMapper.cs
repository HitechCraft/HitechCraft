namespace HitechCraft.WebApplication.Mapper
{
    using Models;
    using BL.CQRS.Command;

    public class ModificationEditViewModelToModificationCreateCommandMapper : BaseMapper<ModificationEditViewModel, ModificationCreateCommand>
    {
        public ModificationEditViewModelToModificationCreateCommandMapper()
        {
            this.ConfigurationStore.CreateMap<ModificationEditViewModel, ModificationCreateCommand>()
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.GuideVideo, ext => ext.MapFrom(src => src.GuideVideoCode))
                .ForMember(dst => dst.Version, ext => ext.MapFrom(src => src.Version));
        }
    }
}