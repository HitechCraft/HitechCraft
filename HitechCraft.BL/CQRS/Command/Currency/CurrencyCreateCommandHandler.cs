using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class CurrencyCreateCommandHandler : BaseCommandHandler<CurrencyCreateCommand>
    {
        public CurrencyCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(CurrencyCreateCommand command)
        {
            var currencyRep = GetRepository<Currency>();
            var playerRep = GetRepository<Player>();

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
