namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;

    #endregion

    public class ShopItemCategoryCreateCommandHandler : BaseCommandHandler<ShopItemCategoryCreateCommand>
    {
        public ShopItemCategoryCreateCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemCategoryCreateCommand command)
        {
            var categoryRep = this.GetRepository<ShopItemCategory>();

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
