﻿using HitechCraft.Core.Databases;
using HitechCraft.Core.Repository.Specification.Currency;

namespace HitechCraft.BL.CQRS.Query
{
    #region Using Directives

    using System.Linq;
    using Core.DI;
    using DAL.Repository;
    using Core.Entity;

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
            var currencyRep = _container.Resolve<IRepository<MySQLConnection, Currency>>();

            return currencyRep.Query(new CurrencyByPlayerNameSpec(query.PlayerName)).First();
        }
    }
}
