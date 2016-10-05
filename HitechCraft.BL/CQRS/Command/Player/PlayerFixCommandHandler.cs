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
        private IContainer _container;

        public PlayerFixCommandHandler(IContainer container) : base(container)
        {
            _container = container;
        }

        public override void Handle(PlayerFixCommand command)
        {
            var playerRep = GetRepository<Player>();
            var currencyRep = GetRepository<Currency>();

            try
            {
                new PlayerAccountCreateCommandHandler(_container)
                    .Handle(new PlayerAccountCreateCommand
                    {
                        Email = command.Email,
                        Gender = command.Gender,
                        Name = command.Name
                    });
            }
            catch (Exception)
            {
            }

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
