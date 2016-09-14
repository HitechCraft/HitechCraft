namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
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
            var currencyRep = this.GetRepository<Currency>();
            var transactionRep = this.GetRepository<IKTransaction>();

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

            if (command.Gonts < 0 && currency.Gonts < command.Gonts) throw new Exception("Недостаточно игровой валюты на счете!");

            if (command.Rubles < 0 && currency.Rubels < command.Rubles) throw new Exception("Недостаточно рублей на счете!");

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
