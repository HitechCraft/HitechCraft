using System;
using System.Linq.Expressions;
using HitechCraft.Core.Entity;
using HitechCraft.Core.Entity.Extentions;

namespace HitechCraft.Core.Repository.Specification.Player
{
    #region Using Directives

    

    #endregion

    public class JobByUuidSpec : ISpecification<Job>
    {
        #region Private Fields

        private readonly byte[] _uuid;

        #endregion

        #region Constructor

        public JobByUuidSpec(byte[] uuid)
        {
            this._uuid = uuid;
        }

        #endregion

        #region Expression

        public Expression<Func<Job, bool>> IsSatisfiedBy()
        {
            //Вот какого фига разработчикам плагина понадобился тип byte[] в базе, ума не приложу!
            return job => job.Uuid.IsEquals(this._uuid);
        }

        #endregion
    }
}
