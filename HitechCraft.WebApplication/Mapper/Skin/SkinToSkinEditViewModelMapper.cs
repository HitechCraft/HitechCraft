namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;

    public class SkinToSkinEditViewModelMapper : BaseMapper<Skin, SkinEditViewModel>
    {
        public SkinToSkinEditViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Skin, SkinEditViewModel>()
                .ForMember(dst => dst.Id, ext => ext.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, ext => ext.MapFrom(src => src.Name))
                .ForMember(dst => dst.Image, ext => ext.MapFrom(src => src.Image))
                .ForMember(dst => dst.Gender, ext => ext.MapFrom(src => src.Gender));

        }
    }
}