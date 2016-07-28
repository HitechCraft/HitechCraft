namespace HitechCraft.Common.CQRS.Query
{
    public interface IQueryExecutor
    {
        TResult Execute<TResult>(IQuery<TResult> query);
    }
}
