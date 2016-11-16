using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using System.Collections.Generic;
    using Core.DI;
    using Core.Entity;
    using System;

    #endregion

    public class ShopItemAddRandomCommandHandler : BaseCommandHandler<ShopItemAddRandomCommand>
    {
        public ShopItemAddRandomCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemAddRandomCommand command)
        {
            var shopItemRep = GetRepository<ShopItem>();
            var playerRep = GetRepository<Player>();
            var playerItemRep = GetRepository<PlayerItem>();
            
            var itemList = (List<ShopItem>)shopItemRep.Query();
            
            Random randForItem = new Random();
            Random randForCount = new Random();

            ShopItem randItem = itemList[randForItem.Next(1, itemList.Count) - 1];
            int randCount = randForCount.Next(1, 64);

            //фикс для рандомной выдачи предмета. лимит 10к Gont
            while (randItem.Price * randCount > 6500)
            {
                randItem = itemList[randForItem.Next(1, itemList.Count) - 1];
                randCount = randForCount.Next(1, 64);
            }

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

