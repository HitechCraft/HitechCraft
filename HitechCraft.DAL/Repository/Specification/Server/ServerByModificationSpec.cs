namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using System.Linq;
    using HitechCraft.Core.Entity;

    #endregion

    public class ServerByModificationSpec : BaseSpecification<Server>
    {
        #region Private Fields

        private readonly int _modId;

        #endregion

        #region Constructor

        public ServerByModificationSpec(int modId)
        {
            this._modId = modId;
        }

        #endregion

        #region Expression

        public override Expression<Func<Server, bool>> IsSatisfiedBy()
        {
            return server => server.ServerModifications.Any(x => x.Modification.Id == this._modId);
        }

        #endregion
    }
}
