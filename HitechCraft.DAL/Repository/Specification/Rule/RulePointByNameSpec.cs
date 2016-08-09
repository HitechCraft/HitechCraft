namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

    #endregion

    public class RulePointByNameSpec : BaseSpecification<RulePoint>
    {
        #region Private Fields

        private readonly string _name;

        #endregion

        #region Constructor

        public RulePointByNameSpec(string name)
        {
            this._name = name;
        }

        #endregion

        #region Expression

        public override Expression<Func<RulePoint, bool>> IsSatisfiedBy()
        {
            return rulePoint => rulePoint.Name == this._name;
        }

        #endregion
    }
}
