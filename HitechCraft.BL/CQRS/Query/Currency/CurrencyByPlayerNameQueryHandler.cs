namespace HitechCraft.BL.CQRS.Query
{
    #region UsingDirectives

    using System.Linq;
    using Common.CQRS.Query;
    using Common.DI;
    using Common.Repository;
    using DAL.Domain;
    using DAL.Repository.Specification;
    using Common.Models.Enum;
    using System;

    #endregion

    public class CurrencyByPlayerNameQueryHandler
        : IQueryHandler<CurrencyByPlayerNameQuery, Currency>
    {
        private readonly IContainer _container;

        public CurrencyByPlayerNameQueryHandler(IContainer container)
        {
            _container = container;
        }

        public Currency Handle(CurrencyByPlayerNameQuery query)
        {
            var currencyRep = this._container.Resolve<IRepository<Currency>>();

            return currencyRep.Query(new CurrencyByPlayerNameSpec(query.PlayerName)).First();
        }
    }
}
