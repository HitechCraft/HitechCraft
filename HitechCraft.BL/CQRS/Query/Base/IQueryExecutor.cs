namespace HitechCraft.BL.CQRS.Query
{
    public interface IQueryExecutor
    {
        TResult Execute<TResult>(IQuery<TResult> query);
    }
}
