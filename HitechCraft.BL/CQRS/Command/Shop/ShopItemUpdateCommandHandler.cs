namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class ShopItemUpdateCommandHandler : BaseCommandHandler<ShopItemUpdateCommand>
    {
        public ShopItemUpdateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemUpdateCommand command)
        {
            var shopItemRep = this.GetRepository<ShopItem>();
            var shopItemCategoryRep = this.GetRepository<ShopItemCategory>();
            var modificationRep = this.GetRepository<Modification>();

            var shopItem = shopItemRep.GetEntity(command.GameId);

            //TODO: в автомаппер
            shopItem.Description = command.Description;
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
