namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    using System;
    using Base;
    using Core.Repository.Specification.Currency;

    #endregion

    public class PlayerFixCommandHandler : BaseCommandHandler<PlayerFixCommand>
    {
        public PlayerFixCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PlayerFixCommand command)
        {
            var playerRep = GetRepository<Player>();
            var currencyRep = GetRepository<Currency>();
            
            if (!currencyRep.Exist(new CurrencyByPlayerNameSpec(command.Name)))
            {
                var currency = new Currency
                {
                    Gonts = 100.00,
                    Rubels = 10.00,
                    Player = playerRep.GetEntity(command.Name),
                    Status = 0
                };

                currencyRep.Add(currency);
            }

            currencyRep.Dispose();
        }
    }
}
