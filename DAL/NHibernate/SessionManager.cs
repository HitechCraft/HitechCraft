using NHibernate;

namespace DAL.NHibernate
{
    /// <summary>
    /// Helper methods for dealing with NHibernate ISession.
    /// </summary>
    public class SessionManager
    {
        /// <summary>
        /// NHibernate Helper
        /// </summary>
        private NHibernateManager nHibernateManager;

        public SessionManager()
        {
            nHibernateManager = new NHibernateManager();
        }

        /// <summary>
        /// Retrieve the current ISession.
        /// </summary>
        public ISession Current
        {
            get
            {
                return nHibernateManager.GetCurrentSession();
            }
        }

        /// <summary>
        /// Create an ISession.
        /// </summary>
        public void CreateSession()
        {
            nHibernateManager.CreateSession();
        }

        /// <summary>
        /// Clear an ISession.
        /// </summary>
        public void ClearSession()
        {
            Current.Clear();
        }

        /// <summary>
        /// Open an ISession.
        /// </summary>
        public void OpenSession()
        {
            nHibernateManager.OpenSession();
        }

        /// <summary>
        /// Close an ISession.
        /// </summary>
        public void CloseSession()
        {
            nHibernateManager.CloseSession();
        }
    }
}
