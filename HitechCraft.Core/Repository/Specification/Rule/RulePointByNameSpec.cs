using System;
using System.Linq.Expressions;
using HitechCraft.Core.Entity;

namespace HitechCraft.Core.Repository.Specification.Rule
{
    #region Using Directives

    

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
