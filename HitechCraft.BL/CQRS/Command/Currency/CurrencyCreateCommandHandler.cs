namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;

    #endregion

    public class CurrencyCreateCommandHandler : BaseCommandHandler<CurrencyCreateCommand>
    {
        public CurrencyCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(CurrencyCreateCommand command)
        {
            var currencyRep = this.GetRepository<Currency>();
            var playerRep = this.GetRepository<Player>();

            currencyRep.Add(new Currency()
            {
                Player = playerRep.GetEntity(command.PlayerName),
                Gonts = command.Gonts,
                Rubels = command.Rubles,
                Status = 0
            });

            currencyRep.Dispose();
        }
    }
}
