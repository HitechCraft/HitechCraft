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

                LogManager.Info("[Player Register] PlayerInfoAdded: " + playerInfo.Email + "; " + playerInfo.Refer + "; ");
            }
            catch (Exception e)
            {
                LogManager.Error("[Player Register] PlayerInfo Not added: " + e.Message);
            }

            LogManager.Info("[Player Register] NickName:" + command.Name);

            if (!String.IsNullOrEmpty(command.ReferId))
            {
                try
                {
                    var refPlayer = playerRep.GetEntity(command.ReferId);
                    playerInfo.Refer = refPlayer;

                    LogManager.Info("[Player Register] Refer:" + refPlayer.Name);
                }
                catch
                {
                    LogManager.Info("[Player Register] Refer none");
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
                LogManager.Info("[Player Register] Player adding error: " + e.Message);
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

                LogManager.Info("[Player Register] Currency created: " + currency.Player.Name);
            }
            catch (Exception e)
            {
                LogManager.Error("[Player Register] Error creating currency: " + e.Message);
            }
            
            playerInfoRep.Dispose();
            playerRep.Dispose();
            currencyRep.Dispose();
        }
    }
}
