using System;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.Permissions
{
    #region Using Directives

    

    #endregion

    public class PermissionsByNameSpec : BaseSpecification<Entity.Permissions>
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

        public override Expression<Func<Entity.Permissions, bool>> IsSatisfiedBy()
        {
            return permissions => permissions.Name == this._name;
        }

        #endregion
    }
}
