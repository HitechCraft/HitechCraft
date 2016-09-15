namespace HitechCraft.DAL.UnitOfWork
{
    #region Using Directives

    using global::NHibernate;
    using NHibernate.Helper;

    #endregion

    /// <summary>
    /// Implementation of IUOW to NHibernate
    /// </summary>
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        public ISession Session { get; set; }

        #region Private Fields

        private ITransaction _transaction;
        private static NHibernateHelper _nhHelper;

        #endregion

        #region Constructors

        static NHibernateUnitOfWork()
        {
            _nhHelper = new NHibernateHelper();
        }

        public NHibernateUnitOfWork()
        {
            this.Session = _nhHelper.CurrentSession;
        }

        #endregion

        #region UOW Actions

        public void BeginTransaction()
        {
            this._transaction = this.Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                // commit transaction if there is one active
                if (this._transaction != null && this._transaction.IsActive)
                    this._transaction.Commit();
            }
            catch
            {
                this.Rollback();

                throw;
            }
            finally
            {
                _nhHelper.CloseSession();
            }
        }

        public void Rollback()
        {
            try
            {
                if (this._transaction != null && this._transaction.IsActive)
                    this._transaction.Rollback();
            }
            finally
            {
                _nhHelper.CloseSession();
            }
        }

        #endregion
    }
}
