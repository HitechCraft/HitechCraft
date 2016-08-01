using System;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;

    #endregion

    public class PlayerRegisterCreateCommandHandler : BaseCommandHandler<PlayerRegisterCreateCommand>
    {
        public PlayerRegisterCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PlayerRegisterCreateCommand command)
        {
            var playerRep = this.GetRepository<Player>();
            var playerInfoRep = this.GetRepository<PlayerInfo>();
            var currencyRep = this.GetRepository<Currency>();

            var playerInfo = new PlayerInfo()
            {
                Email = command.Email
            };

            if (!String.IsNullOrEmpty(command.ReferId))
            {
                try
                {
                    var refPlayer = playerRep.GetEntity(command.ReferId);
                    playerInfo.Refer = refPlayer;
                }
                catch
                {

                }
            }
            
            var player = new Player()
            {
                Name = command.Name,
                Gender = command.Gender,
                Info = playerInfo
            };

            var currency = new Currency()
            {
                Gonts = 100.00,
                Rubels = 10.00,
                Player = player,
                Status = 0
            };

            playerInfoRep.Add(playerInfo);

            playerRep.Add(player);

            currencyRep.Add(currency);

            playerInfoRep.Dispose();
            playerRep.Dispose();
            currencyRep.Dispose();
        }
    }
}
