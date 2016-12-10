namespace HitechCraft.BL.CQRS.Query.Base
{
    public interface IQueryExecutor
    {
        TResult Execute<TResult>(IQuery<TResult> query);
    }
}
