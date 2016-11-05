using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class ShopItemUpdateCommandHandler : BaseCommandHandler<ShopItemUpdateCommand>
    {
        public ShopItemUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemUpdateCommand command)
        {
            var shopItemRep = GetRepository<ShopItem>();
            var shopItemCategoryRep = GetRepository<ShopItemCategory>();
            var modificationRep = GetRepository<Modification>();

            var shopItem = shopItemRep.GetEntity(command.GameId);

            //TODO: в автомаппер
            shopItem.Image = command.Image ?? shopItem.Image;
            shopItem.ItemCategory = shopItemCategoryRep.GetEntity(command.CategoryId);
            shopItem.Modification = modificationRep.GetEntity(command.ModificationId);
            shopItem.Name = command.Name;
            shopItem.Price = command.Price;

            shopItemRep.Update(shopItem);
            shopItemRep.Dispose();
        }
    }
}
