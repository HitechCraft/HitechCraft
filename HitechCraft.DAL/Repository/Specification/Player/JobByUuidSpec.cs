namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Core.Entity;
    using Core.Entity.Extentions;

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
