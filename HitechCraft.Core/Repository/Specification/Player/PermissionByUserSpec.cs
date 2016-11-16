namespace HitechCraft.Core.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Helper;

    #endregion

    public class PermissionByUserSpec : ISpecification<Entity.Permissions>
    {
        #region Private Fields

        private readonly string _userName;

        #endregion

        #region Constructor

        public PermissionByUserSpec(string userName)
        {
            this._userName = userName;
        }

        #endregion

        #region Expression

        public Expression<Func<Entity.Permissions, bool>> IsSatisfiedBy()
        {
            return permissions => permissions.Name == HashHelper.UuidFromString("OfflinePlayer:" + this._userName) && permissions.Permission == "name" && permissions.Value == this._userName;
        }

        #endregion
    }
}
