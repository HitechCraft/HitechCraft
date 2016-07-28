namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

    #endregion

    public class PermissionsByGroupContainsSpec : BaseSpecification<Permissions>
    {
        #region Private Fields

        private readonly string _permission;

        #endregion

        #region Constructor

        public PermissionsByGroupContainsSpec()
        {
        }

        #endregion

        #region Expression

        public override Expression<Func<Permissions, bool>> IsSatisfiedBy()
        {
            return permissions => permissions.Permission.Contains("group") && permissions.Permission.Contains("until");
        }

        #endregion
    }
}
