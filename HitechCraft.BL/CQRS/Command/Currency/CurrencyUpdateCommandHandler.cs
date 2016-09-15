using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    using System;
    using System.Linq;
    using DAL.Repository.Specification;

    #endregion

    public class CurrencyUpdateCommandHandler : BaseCommandHandler<CurrencyUpdateCommand>
    {
        public CurrencyUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(CurrencyUpdateCommand command)
        {
            var currencyRep = GetRepository<Currency>();
            var transactionRep = GetRepository<IKTransaction>();

            Currency currency;

            if (command.Id != 0 && String.IsNullOrEmpty(command.TransactionId))
            {
                currency = currencyRep.GetEntity(command.Id);
            }
            else
            {
                var transaction = transactionRep.GetEntity(command.TransactionId);

                currency = currencyRep.Query(new CurrencyByPlayerNameSpec(transaction.Player.Name)).First();
            }

            if (command.Gonts < 0 && currency.Gonts < Math.Abs(command.Gonts)) throw new Exception("Недостаточно игровой валюты на счете!");

            if (command.Rubles < 0 && currency.Rubels < Math.Abs(command.Rubles)) throw new Exception("Недостаточно рублей на счете!");

            if (command.Gonts != 0)
            {
                currency.Gonts += command.Gonts;
            }

            if (command.Rubles != 0)
            {
                currency.Rubels += command.Rubles;
            }

            currencyRep.Update(currency);
            currencyRep.Dispose();
        }
    }
}
