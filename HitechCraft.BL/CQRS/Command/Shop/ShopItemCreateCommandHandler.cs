using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class ShopItemCreateCommandHandler : BaseCommandHandler<ShopItemCreateCommand>
    {
        public ShopItemCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemCreateCommand command)
        {
            var shopItemRep = GetRepository<ShopItem>();
            var categoryRep = GetRepository<ShopItemCategory>();
            var modificationRep = GetRepository<Modification>();

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
