using HitechCraft.BL.CQRS.Command.Base;
using HitechCraft.Core.Repository.Specification.Shop;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using System.Linq;
    using Core.DI;
    using Core.Entity;
    using System;

    #endregion

    public class ShopItemCategoryRemoveCommandHandler : BaseCommandHandler<ShopItemCategoryRemoveCommand>
    {
        public ShopItemCategoryRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemCategoryRemoveCommand command)
        {
            var shopItemCategoryRep = GetRepository<ShopItemCategory>();
            var shopItemRep = GetRepository<ShopItem>();

            if (shopItemRep.Query(new ShopItemByCategorySpec(command.CategoryId)).Any())
            {
                throw new Exception("Категория используется предметами");
            }

            shopItemCategoryRep.Delete(command.CategoryId);

            shopItemCategoryRep.Dispose();
            shopItemRep.Dispose();
        }
    }
}
