namespace HitechCraft.Core.Repository.Specification.Player
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Entity;
    using HitechCraft.Core.Entity.Extentions;

    #endregion

    public class ReferalByRefererSpec : ISpecification<Referal>
    {
        #region Private Fields

        private readonly string _refererName;

        #endregion

        #region Constructor

        public ReferalByRefererSpec(string refererName)
        {
            this._refererName = refererName;
        }

        #endregion

        #region Expression

        public Expression<Func<Referal, bool>> IsSatisfiedBy()
        {
            return referal => referal.Referer.Name == _refererName;
        }

        #endregion
    }
}
