namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class ShopItemCreateCommandHandler : BaseCommandHandler<ShopItemCreateCommand>
    {
        public ShopItemCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemCreateCommand command)
        {
            var shopItemRep = this.GetRepository<ShopItem>();
            var categoryRep = this.GetRepository<ShopItemCategory>();
            var modificationRep = this.GetRepository<Modification>();

            var shopItem = new ShopItem()
            {
                Description = command.Description,
                GameId = command.GameId,
                Image = command.Image,
                Name = command.Name,
                ItemCategory = categoryRep.GetEntity(command.CategoryId),
                Modification = modificationRep.GetEntity(command.ModificationId),
                Price = command.Price
            };

            shopItemRep.Add(shopItem);
            shopItemRep.Dispose();
        }
    }
}
