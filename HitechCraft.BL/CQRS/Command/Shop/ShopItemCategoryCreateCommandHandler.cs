using HitechCraft.BL.CQRS.Command.Base;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Core.DI;
    using Core.Entity;

    #endregion

    public class ShopItemCategoryCreateCommandHandler : BaseCommandHandler<ShopItemCategoryCreateCommand>
    {
        public ShopItemCategoryCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemCategoryCreateCommand command)
        {
            var categoryRep = GetRepository<ShopItemCategory>();

            var shopItemCategory = new ShopItemCategory()
            {
                Description = command.Description,
                Name = command.Name,
            };

            categoryRep.Add(shopItemCategory);
            categoryRep.Dispose();
        }
    }
}
