namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

    #endregion

    public class IKTransactionByTransactionIdSpec : ISpecification<IKTransaction>
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

        public Expression<Func<IKTransaction, bool>> IsSatisfiedBy()
        {
            return ikTransaction => ikTransaction.TransactionId == this._transactionId;
        }

        #endregion
    }
}
