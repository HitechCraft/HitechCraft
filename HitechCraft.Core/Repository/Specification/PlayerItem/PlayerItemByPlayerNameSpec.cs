using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.PlayerItem
{
    #region Using Directives

    

    #endregion

    public class PlayerItemByPlayerNameSpec : ISpecification<Entity.PlayerItem>
    {
        #region Private Fields

        private readonly string _playerName;

        #endregion

        #region Constructor

        public PlayerItemByPlayerNameSpec(string playerName)
        {
            this._playerName = playerName;
        }

        #endregion

        #region Expression

        public Expression<Func<Entity.PlayerItem, bool>> IsSatisfiedBy()
        {
            return playerItem => playerItem.Player.Name == this._playerName;
        }

        #endregion
    }
}
