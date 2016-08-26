namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

    #endregion

    public class PlayerItemByPlayerNameSpec : ISpecification<PlayerItem>
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

        public Expression<Func<PlayerItem, bool>> IsSatisfiedBy()
        {
            return playerItem => playerItem.Player.Name == this._playerName;
        }

        #endregion
    }
}
