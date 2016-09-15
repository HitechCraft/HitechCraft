using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.Permissions
{
    #region Using Directives

    

    #endregion

    public class PermissionsByGroupContainsSpec : BaseSpecification<Entity.Permissions>
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

        public override Expression<Func<Entity.Permissions, bool>> IsSatisfiedBy()
        {
            return permissions => permissions.Permission.Contains("group") && permissions.Permission.Contains("until");
        }

        #endregion
    }
}
