using System;
using System.Linq;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.Server
{
    #region Using Directives

    

    #endregion

    public class ServerByModificationSpec : BaseSpecification<Entity.Server>
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

        public override Expression<Func<Entity.Server, bool>> IsSatisfiedBy()
        {
            return server => server.ServerModifications.Any(x => x.Modification.Id == this._modId);
        }

        #endregion
    }
}
