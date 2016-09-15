using System;
using System.Linq.Expressions;
using HitechCraft.Core.Entity;

namespace HitechCraft.Core.Repository.Specification.Shop
{
    #region Using Directives

    

    #endregion

    public class ShopItemByCategorySpec : BaseSpecification<ShopItem>
    {
        #region Private Fields

        private readonly int _categoryId;

        #endregion

        #region Constructor

        public ShopItemByCategorySpec(int categoryId)
        {
            this._categoryId = categoryId;
        }

        #endregion

        #region Expression

        public override Expression<Func<ShopItem, bool>> IsSatisfiedBy()
        {
            return shopItem => shopItem.ItemCategory.Id == this._categoryId;
        }

        #endregion
    }
}
