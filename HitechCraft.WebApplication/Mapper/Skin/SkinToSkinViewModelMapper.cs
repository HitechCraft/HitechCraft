using HitechCraft.Core.Entity;

namespace HitechCraft.WebApplication.Mapper
{
    using Models;

    public class SkinToSkinViewModelMapper : BaseMapper<Skin, SkinViewModel>
    {
        public SkinToSkinViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Skin, SkinViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.Gender, ext => ext.MapFrom(src => src.Gender));

        }
    }
}