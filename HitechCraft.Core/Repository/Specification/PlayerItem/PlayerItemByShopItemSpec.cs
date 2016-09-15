using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.PlayerItem
{
    #region Using Directives

    

    #endregion

    public class PlayerItemByShopItemSpec : ISpecification<Entity.PlayerItem>
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

        public Expression<Func<Entity.PlayerItem, bool>> IsSatisfiedBy()
        {
            return playerItem => playerItem.Item.GameId == this._shopItemId;
        }

        #endregion
    }
}
