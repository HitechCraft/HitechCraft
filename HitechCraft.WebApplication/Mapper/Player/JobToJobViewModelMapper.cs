namespace HitechCraft.WebApplication.Mapper
{
    using DAL.Domain;
    using Models;

    public class JobToJobViewModelMapper : BaseMapper<Job, JobViewModel>
    {
        public JobToJobViewModelMapper()
        {
            this.ConfigurationStore.CreateMap<Job, JobViewModel>()
                .ForMember(dst => dst.Uuid, ext => ext.MapFrom(src => src.Uuid))
                .ForMember(dst => dst.JobName, ext => ext.MapFrom(src => src.JobName))
                .ForMember(dst => dst.Experiance, ext => ext.MapFrom(src => src.Experiance))
                .ForMember(dst => dst.Level, ext => ext.MapFrom(src => src.Level));
        }
    }
}