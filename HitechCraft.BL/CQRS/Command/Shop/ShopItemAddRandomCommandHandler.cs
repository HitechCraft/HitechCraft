using System.Collections.Generic;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;
    using System.Linq;
    using DAL.Repository.Specification;

    #endregion

    public class ShopItemAddRandomCommandHandler : BaseCommandHandler<ShopItemAddRandomCommand>
    {
        public ShopItemAddRandomCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemAddRandomCommand command)
        {
            var shopItemRep = this.GetRepository<ShopItem>();
            var playerRep = this.GetRepository<Player>();
            var playerItemRep = this.GetRepository<PlayerItem>();
            
            var itemList = (List<ShopItem>)shopItemRep.Query();
            
            Random randForItem = new Random();
            Random randForCount = new Random();

            var randItem = itemList[randForItem.Next(1, itemList.Count) - 1];
            var randCount = randForCount.Next(1, 64);

            var playerItem = new PlayerItem()
            {
                Count = randCount,
                Item = randItem,
                Player = playerRep.GetEntity(command.PlayerName)
            };
            
            playerItemRep.Add(playerItem);
            
            playerItemRep.Dispose();
        }
    }
}

