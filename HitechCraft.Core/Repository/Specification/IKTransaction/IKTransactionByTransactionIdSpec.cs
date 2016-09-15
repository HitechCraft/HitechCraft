using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.IKTransaction
{
    #region Using Directives

    

    #endregion

    public class IKTransactionByTransactionIdSpec : ISpecification<Entity.IKTransaction>
    {
        #region Private Fields

        private readonly string _transactionId;

        #endregion

        #region Constructor

        public IKTransactionByTransactionIdSpec(string transactionId)
        {
            this._transactionId = transactionId;
        }

        #endregion

        #region Expression

        public Expression<Func<Entity.IKTransaction, bool>> IsSatisfiedBy()
        {
            return ikTransaction => ikTransaction.TransactionId == this._transactionId;
        }

        #endregion
    }
}
