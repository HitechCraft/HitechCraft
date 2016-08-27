namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

    #endregion

    public class PermissionsByNameSpec : BaseSpecification<Permissions>
    {
        #region Private Fields

        private readonly string _name;

        #endregion

        #region Constructor

        public PermissionsByNameSpec(string name)
        {
            this._name = name;
        }

        #endregion

        #region Expression

        public override Expression<Func<Permissions, bool>> IsSatisfiedBy()
        {
            return permissions => permissions.Name == this._name;
        }

        #endregion
    }
}
