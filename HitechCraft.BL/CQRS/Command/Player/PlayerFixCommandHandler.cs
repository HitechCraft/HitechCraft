using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;
    using Core.Helper;
    using System;

    #endregion

    public class PlayerFixCommandHandler : BaseCommandHandler<PlayerFixCommand>
    {
        public PlayerFixCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PlayerFixCommand command)
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
                LogHelper.Error("PlayerInfo Not added: " + e.Message, "Player Fix");
            }

            try
            {
                var player = new Player()
                {
                    Name = command.Name,
                    Gender = command.Gender,
                    Info = playerInfo
                };

                playerRep.Add(player);
            }
            catch (Exception e)
            {
                LogHelper.Error("Player Not added: " + e.Message, "Player Fix");
            }

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
                LogHelper.Error("Currency Not added: " + e.Message, "Player Fix");
            }

            playerInfoRep.Dispose();
            playerRep.Dispose();
            currencyRep.Dispose();
        }
    }
}
