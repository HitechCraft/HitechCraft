using System.Text;

namespace HitechCraft.DAL.Repository.Specification
{
    #region Using Directives

    using System;
    using System.Linq.Expressions;
    using Common.Repository.Specification;
    using Domain;

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
            var uuid = Encoding.UTF8.GetString(this._uuid);
            
            //Вот какого фига разработчикам плагина понадобился тип byte[] в базе, ума не приложу!
            return job => Encoding.UTF8.GetString(job.Uuid) == uuid;
        }

        #endregion
    }
}
