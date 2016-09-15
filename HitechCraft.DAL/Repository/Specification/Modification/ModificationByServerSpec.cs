namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using System.Linq;
    using Core.Entity;

    #endregion

    public class ModificationByServerSpec : BaseSpecification<Modification>
    {
        #region Private Fields

        private readonly int _serverId;

        #endregion

        #region Constructor

        public ModificationByServerSpec(int serverId)
        {
            this._serverId = serverId;
        }

        #endregion

        #region Expression

        public override Expression<Func<Modification, bool>> IsSatisfiedBy()
        {
            return mod => mod.ServerModifications.Any(x => x.Server.Id == this._serverId);
        }

        #endregion
    }
}
