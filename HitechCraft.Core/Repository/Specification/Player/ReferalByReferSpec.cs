namespace HitechCraft.Core.Repository.Specification.Player
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Entity;
    using HitechCraft.Core.Entity.Extentions;

    #endregion

    public class ReferalByReferSpec : ISpecification<Referal>
    {
        #region Private Fields

        private readonly string _referName;

        #endregion

        #region Constructor

        public ReferalByReferSpec(string referName)
        {
            this._referName = referName;
        }

        #endregion

        #region Expression

        public Expression<Func<Referal, bool>> IsSatisfiedBy()
        {
            return referal => referal.Refer.Name == _referName;
        }

        #endregion
    }
}
