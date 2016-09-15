﻿using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using System;
    using System.Linq;
    using Core.DI;
    using Core.Entity;
    using DAL.Repository.Specification;

    #endregion

    public class PlayerInfoUpdateCommandHandler : BaseCommandHandler<PlayerInfoUpdateCommand>
    {
        public PlayerInfoUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(PlayerInfoUpdateCommand command)
        {
            var playerRep = this.GetRepository<Player>();
            var currencyRep = this.GetRepository<Currency>();

            var player = playerRep.GetEntity(command.Name);

            player.Gender = command.Gender;
            playerRep.Update(player);

            try
            {
                var playerCurrency = currencyRep.Query(new CurrencyByPlayerNameSpec(command.Name)).First();

                playerCurrency.Gonts = command.Gonts;
                playerCurrency.Rubels = command.Rubles;

                currencyRep.Update(playerCurrency);
            }
            catch (Exception e)
            {
                throw new Exception("Ошибка обновления счета: " + e.Message);
            }

            playerRep.Dispose();
            currencyRep.Dispose();
        }
    }
}
