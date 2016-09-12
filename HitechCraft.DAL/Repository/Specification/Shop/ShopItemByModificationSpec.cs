namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

    #endregion

    public class ShopItemByModificationSpec : BaseSpecification<ShopItem>
    {
        #region Private Fields

        private readonly int _modId;

        #endregion

        #region Constructor

        public ShopItemByModificationSpec(int modId)
        {
            this._modId = modId;
        }

        #endregion

        #region Expression

        public override Expression<Func<ShopItem, bool>> IsSatisfiedBy()
        {
            return shopItem => shopItem.Modification.Id == this._modId;
        }

        #endregion
    }
}
