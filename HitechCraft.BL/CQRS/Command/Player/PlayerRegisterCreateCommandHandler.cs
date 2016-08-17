namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;
    using HitechCraft.Common.Core;

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

            try
            {
                playerInfoRep.Add(playerInfo);
            }
            catch (Exception e)
            {
                LogManager.Error("PlayerInfo Not added: " + e.Message, "Player Register");
            }
            
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
                LogManager.Error("Player adding error: " + e.Message, "Player Register");
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
                LogManager.Error("Error creating currency: " + e.Message, "Player Register");
            }
            
            playerInfoRep.Dispose();
            playerRep.Dispose();
            currencyRep.Dispose();
        }
    }
}
