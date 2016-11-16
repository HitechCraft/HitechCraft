namespace HitechCraft.Core.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Helper;

    #endregion

    public class PexEntityByUserSpec : ISpecification<Entity.PexEntity>
    {
        #region Private Fields

        private readonly string _userName;

        #endregion

        #region Constructor

        public PexEntityByUserSpec(string userName)
        {
            this._userName = userName;
        }

        #endregion

        #region Expression

        public Expression<Func<Entity.PexEntity, bool>> IsSatisfiedBy()
        {
            return permissions => permissions.Name == HashHelper.UuidFromString("OfflinePlayer:" + this._userName) && permissions.Type == 1;
        }

        #endregion
    }
}
