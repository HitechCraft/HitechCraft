using System;
using System.Linq;
using System.Linq.Expressions;

namespace HitechCraft.Core.Repository.Specification.Modification
{
    #region Using Directives

    

    #endregion

    public class ModificationByServerSpec : BaseSpecification<Entity.Modification>
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

        public override Expression<Func<Entity.Modification, bool>> IsSatisfiedBy()
        {
            return mod => mod.ServerModifications.Any(x => x.Server.Id == this._serverId);
        }

        #endregion
    }
}
