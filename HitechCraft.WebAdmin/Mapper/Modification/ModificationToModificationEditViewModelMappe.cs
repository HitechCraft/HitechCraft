﻿namespace HitechCraft.WebAdmin.Mapper
{
    using Models;

    public class ModificationToModificationEditViewModelMapper : BaseMapper<Core.Entity.Modification, ModificationEditViewModel>
    {
        public ModificationToModificationEditViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Core.Entity.Modification, ModificationEditViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, ext => ext.MapFrom(src => src.Description))
                .ForMember(dst => dst.Version, ext => ext.MapFrom(src => src.Version))
                .ForMember(dst => dst.GuideVideoCode, ext => ext.MapFrom(src => src.GuideVideo))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image));
        }
    }
}