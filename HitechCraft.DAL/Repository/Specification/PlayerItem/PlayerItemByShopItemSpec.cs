namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using HitechCraft.Core.Entity;

    #endregion

    public class PlayerItemByShopItemSpec : ISpecification<PlayerItem>
    {
        #region Private Fields

        private readonly string _shopItemId;

        #endregion

        #region Constructor

        public PlayerItemByShopItemSpec(string shopItemId)
        {
            this._shopItemId = shopItemId;
        }

        #endregion

        #region Expression

        public Expression<Func<PlayerItem, bool>> IsSatisfiedBy()
        {
            return playerItem => playerItem.Item.GameId == this._shopItemId;
        }

        #endregion
    }
}
