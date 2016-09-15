using HitechCraft.BL.CQRS.Command.Base;
using HitechCraft.Core.Repository.Specification.PlayerItem;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using System;
    using System.Linq;
    using Core.DI;
    using Core.Entity;

    #endregion

    public class ShopItemRemoveCommandHandler : BaseCommandHandler<ShopItemRemoveCommand>
    {
        public ShopItemRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemRemoveCommand command)
        {
            var shopItemRep = GetRepository<ShopItem>();
            var playerItemRep = GetRepository<PlayerItem>();

            if (playerItemRep.Query(new PlayerItemByShopItemSpec(command.GameId)).Any())
            {
                throw new Exception("Предмет используется пользователями");
            }

            shopItemRep.Delete(command.GameId);
            shopItemRep.Dispose();
        }
    }
}
