using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using System;
    using System.Linq;
    using Core.DI;
    using Core.Entity;
    using DAL.Repository.Specification;

    #endregion

    public class ShopItemBuyCommandHandler : BaseCommandHandler<ShopItemBuyCommand>
    {
        public ShopItemBuyCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemBuyCommand command)
        {
            var shopItemRep = GetRepository<ShopItem>();
            var playerRep = GetRepository<Player>();
            var currencyRep = GetRepository<Currency>();
            var playerItemRep = GetRepository<PlayerItem>();
            
            var item = shopItemRep.GetEntity(command.GameId);

            var currency = currencyRep.Query(new CurrencyByPlayerNameSpec(command.PlayerName)).First();

            if (item.Price*command.Count > currency.Gonts)
                throw new Exception("Недостаточно денег на счете");

            currency.Gonts -= Math.Round((double)item.Price * command.Count, 2);

            var playerItem = new PlayerItem()
            {
                Count = command.Count,
                Item = item,
                Player = playerRep.GetEntity(command.PlayerName)
            };

            currencyRep.Update(currency);
            playerItemRep.Add(playerItem);

            currencyRep.Dispose();
            playerItemRep.Dispose();
        }
    }
}

