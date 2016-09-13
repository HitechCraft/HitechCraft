using HitechCraft.WebAdmin.Models;

namespace HitechCraft.WebAdmin.Mapper
{
    public class SkinToSkinViewModelMapper : BaseMapper<DAL.Domain.Skin, SkinViewModel>
    {
        public SkinToSkinViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<DAL.Domain.Skin, SkinViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.Gender, ext => ext.MapFrom(src => src.Gender));

        }
    }
}