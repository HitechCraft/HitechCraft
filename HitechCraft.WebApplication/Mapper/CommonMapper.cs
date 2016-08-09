namespace HitechCraft.WebApplication.Mapper
{
    public class CommonMapper<TSource, TResult> : BaseMapper<TSource, TResult>
    {
        public CommonMapper()
        {
            this.ConfigurationStore.CreateMap<TSource, TResult>();
        }
    }
}