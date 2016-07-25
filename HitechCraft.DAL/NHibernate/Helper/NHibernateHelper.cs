namespace HitechCraft.DAL.NHibernate.Helper
{
    #region Using Directives
    
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using global::NHibernate;
    using global::NHibernate.Context;

    #endregion

    /// <summary>
    /// Here basic NHibernate manipulation methods are implemented.
    /// </summary>
    public class NHibernateHelper
    {
        private ISessionFactory _sessionFactory;

        /// <summary>
        /// In case there is an already instantiated NHibernate ISessionFactory,
        /// retrieve it, otherwise instantiate it.
        /// </summary>
        public ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Fluently.Configure()
                        .Database(MySQLConfiguration.Standard.ConnectionString(x => x.FromConnectionStringWithKey("MySQLConnection")))
                        .Mappings(x => x.AutoMappings(new AutomappingHelper()))
                        .ExposeConfiguration(config => config.SetProperty("current_session_context_class", "web"))
                        .BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public ISession CurrentSession
        {
            get
            {
                return this.GetCurrentSession();
            }
        }

        /// <summary>
        /// Open an ISession based on the built SessionFactory.
        /// </summary>
        /// <returns>Opened ISession.</returns>
        public ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
        /// <summary>
        /// Create an ISession and bind it to the current tNHibernate Context.
        /// </summary>
        public void CreateSession()
        {
            CurrentSessionContext.Bind(OpenSession());
        }

        /// <summary>
        /// Close an ISession and unbind it from the current
        /// NHibernate Context.
        /// </summary>
        public void CloseSession()
        {
            if (CurrentSessionContext.HasBind(SessionFactory))
            {
                CurrentSessionContext.Unbind(SessionFactory).Dispose();
            }
        }

        /// <summary>
        /// Retrieve the current binded NHibernate ISession, in case there
        /// is any. Otherwise, open a new ISession.
        /// </summary>
        /// <returns>The current binded NHibernate ISession.</returns>
        public ISession GetCurrentSession()
        {
            if (!CurrentSessionContext.HasBind(SessionFactory))
            {
                this.CreateSession();
            }

            return SessionFactory.GetCurrentSession();
        }
    }
}
