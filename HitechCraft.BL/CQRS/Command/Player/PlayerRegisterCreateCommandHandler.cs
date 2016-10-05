using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    using Core.Helper;
    using System;

    #endregion

    public class PlayerRegisterCreateCommandHandler : BaseCommandHandler<PlayerRegisterCreateCommand>
    {
        public PlayerRegisterCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PlayerRegisterCreateCommand command)
        {
            var playerRep = GetRepository<Player>();
            var playerInfoRep = GetRepository<PlayerInfo>();
            var currencyRep = GetRepository<Currency>();

            var playerInfo = new PlayerInfo()
            {
                Email = command.Email
            };

            try
            {
                playerInfoRep.Add(playerInfo);
            }
            catch (Exception e)
            {
                LogHelper.Error("PlayerInfo Not added: " + e.Message, "Player Register");
            }

            var player = new Player()
            {
                Name = command.Name,
                Gender = command.Gender,
                Info = playerInfo
            };

            playerRep.Add(player);

            try
            {
                var currency = new Currency()
                {
                    Gonts = 100.00,
                    Rubels = 10.00,
                    Player = playerRep.GetEntity(command.Name),
                    Status = 0
                };

                currencyRep.Add(currency);
            }
            catch (Exception e)
            {
                LogHelper.Error("Error creating currency: " + e.Message, "Player Register");
            }

            playerInfoRep.Dispose();
            playerRep.Dispose();

            if (!String.IsNullOrEmpty(command.ReferId) && playerRep.Exist(command.ReferId))
            {
                var referalRep = GetRepository<Referal>();
                var referal = new Referal()
                {
                    Refer = playerRep.GetEntity(command.ReferId),
                    Referer = player
                };

                referalRep.Add(referal);
                referalRep.Dispose();
            }

            currencyRep.Dispose();
        }
    }
}
