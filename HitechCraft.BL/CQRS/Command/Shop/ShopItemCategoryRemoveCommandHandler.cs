using System.Linq;

namespace HitechCraft.BL.CQRS.Command
{
    #region Using Directives

    using Common.CQRS.Command;
    using Common.DI;
    using DAL.Domain;
    using System;
    using HitechCraft.DAL.Repository.Specification;

    #endregion

    public class ShopItemCategoryRemoveCommandHandler : BaseCommandHandler<ShopItemCategoryRemoveCommand>
    {
        public ShopItemCategoryRemoveCommandHandler(IContainer container) : base(container)
        {
        }

        public override void Handle(ShopItemCategoryRemoveCommand command)
        {
            var shopItemCategoryRep = this.GetRepository<ShopItemCategory>();
            var shopItemRep = this.GetRepository<ShopItem>();

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
