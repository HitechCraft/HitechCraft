namespace HitechCraft.WebAdmin.Mapper
{
    public class CommonMapper<TSource, TResult> : BaseMapper<TSource, TResult>
    {
        public CommonMapper()
        {
            this.ConfigurationStore.CreateMap<TSource, TResult>();
        }
    }
}