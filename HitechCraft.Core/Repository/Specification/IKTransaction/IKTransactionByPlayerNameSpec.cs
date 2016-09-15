using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.IKTransaction
{
    #region Using Directives

    

    #endregion

    public class IKTransactionByPlayerNameSpec : ISpecification<Entity.IKTransaction>
    {
        #region Private Fields

        private readonly string _playerName;

        #endregion

        #region Constructor

        public IKTransactionByPlayerNameSpec(string playerName)
        {
            this._playerName = playerName;
        }

        #endregion

        #region Expression

        public Expression<Func<Entity.IKTransaction, bool>> IsSatisfiedBy()
        {
            return ikTransaction => ikTransaction.Player.Name == this._playerName;
        }

        #endregion
    }
}
