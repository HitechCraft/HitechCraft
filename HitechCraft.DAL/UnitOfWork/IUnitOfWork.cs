using NHibernate;

namespace HitechCraft.DAL.UnitOfWork
{
    #region Using Directives

    

    #endregion

    public interface IUnitOfWork<TDataBase>
    {
        /// <summary>
        /// Session NHibernate
        /// </summary>
        ISession Session { get; set; }
        /// <summary>
        /// Begin NHibernate transaction
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// Commit changes
        /// </summary>
        void Commit();
        /// <summary>
        /// Rollback transaction
        /// </summary>
        void Rollback();
    }
}
