namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;

    #endregion

    public class CurrencyUpdateCommandHandler : BaseCommandHandler<CurrencyUpdateCommand>
    {
        public CurrencyUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(CurrencyUpdateCommand command)
        {
            var currencyRep = this.GetRepository<Currency>();

            var currency = currencyRep.GetEntity(command.Id);

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
