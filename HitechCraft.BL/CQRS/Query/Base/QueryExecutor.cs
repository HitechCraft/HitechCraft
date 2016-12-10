namespace HitechCraft.BL.CQRS.Query.Base
{
    using HitechCraft.Core.DI;

    public class QueryExecutor : IQueryExecutor
    {
        private readonly IContainer _container;

        public QueryExecutor(IContainer container)
        {
            this._container = container;
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            var handler = this._container.Resolve<IQueryHandler<IQuery<TResult>, TResult>>();

            return handler.Handle(query);
        }
    }
}
