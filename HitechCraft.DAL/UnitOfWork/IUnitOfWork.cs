namespace HitechCraft.Common.UnitOfWork
{
    #region Using Directives

    using NHibernate;

    #endregion

    public interface IUnitOfWork
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
