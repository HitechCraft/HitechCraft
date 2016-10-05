using HitechCraft.BL.CQRS.Command.Base;
using HitechCraft.Core.Repository.Specification.Currency;
using HitechCraft.Core.Repository.Specification.Player;

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

            var playerInfo = new PlayerInfo();
            var player = new Player();
            var currency = new Currency();

            if (!playerInfoRep.Exist(new PlayerInfoByEmailSpec(command.Email)))
            {
                playerInfo.Email = command.Email;

                playerInfoRep.Add(playerInfo);
                playerInfoRep.Dispose();
            }

            if (!playerRep.Exist(new PlayerByLoginSpec(command.Name)))
            {
                player.Name = command.Name;
                player.Gender = command.Gender;
                player.Info = playerInfo;

                playerRep.Add(player);
                playerRep.Dispose();
            }

            if (!currencyRep.Exist(new CurrencyByPlayerNameSpec(command.Name)))
            {
                currency.Gonts = 100.00;
                currency.Rubels = 10.00;
                currency.Player = playerRep.GetEntity(command.Name);
                currency.Status = 0;

                currencyRep.Add(currency);
                currencyRep.Dispose();
            }
        }
    }
}
