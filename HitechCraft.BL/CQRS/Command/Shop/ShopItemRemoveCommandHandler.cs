using System.Linq;
using HitechCraft.DAL.Repository.Specification;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class ShopItemRemoveCommandHandler : BaseCommandHandler<ShopItemRemoveCommand>
    {
        public ShopItemRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemRemoveCommand command)
        {
            var shopItemRep = this.GetRepository<ShopItem>();
            var playerItemRep = this.GetRepository<PlayerItem>();

            if (playerItemRep.Query(new PlayerItemByShopItemSpec(command.GameId)).Any())
            {
                throw new Exception("Предмет используется пользователями. Удаление невозможно!");
            }

            shopItemRep.Delete(command.GameId);
            shopItemRep.Dispose();
        }
    }
}
