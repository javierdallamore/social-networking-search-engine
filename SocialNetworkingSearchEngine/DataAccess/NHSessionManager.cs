using System.Web;
using DataAccess.Mapping;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using System.Runtime.Remoting.Messaging;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace DataAccess
{
    public sealed class NHSessionManager
    {
        public static bool CreateDB = false;
        private string connectionString;

        public static NHSessionManager Instance
        {
            get
            {
                return Nested.SessionManager;
            }
        }

        private NHSessionManager()
        {
            InitSessionFactory();
        }

        private class Nested
        {
            static Nested() { }
            internal static readonly NHSessionManager SessionManager = new NHSessionManager();
        }

        /// <summary>
        /// Lee la cadena de conexion desde el Web.config.
        /// </summary>
        private void ReadDataAccess()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SocialNetworkingSearchEngineDB"].ConnectionString;
        }

        private void InitSessionFactory()
        {
            ReadDataAccess();

            FluentConfiguration fc;

            fc = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString));
            //.ShowSql()
            //.ProxyFactoryFactory<ProxyFactoryFactory>());
            fc.Mappings(m => m.FluentMappings.AddFromAssemblyOf<PostMap>());
            fc.ExposeConfiguration(BuildSchema);

            sessionFactory = fc.BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            //config.Properties.Add("cache.use_second_level_cache", "true");
            //config.Properties.Add("cache.use_query_cache", "true");
            //config.Properties.Add("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");

            if (CreateDB)
            {
                var ex = new SchemaExport(config);
                ex.Drop(false, true);
                ex.Create(false, true);
                CreateDB = false;
            }
            else
            {
                //SchemaValidator validador = new SchemaValidator(config);
                //var errores = validador.Validate();

                //StreamWriter writer = new StreamWriter(@"D:\mapeo_siage.txt");
                //foreach (var error in errores)
                //    writer.WriteLine(error);
                //writer.Close();
            }

        }

        /// <summary>
        /// Allows you to register an interceptor on a new session.  This may not be called if there is already
        /// an open session attached to the HttpContext.  If you have an interceptor to be used, modify
        /// the HttpModule to call this before calling BeginTransaction().
        /// </summary>
        public void RegisterInterceptor(IInterceptor interceptor)
        {
            ISession session = ContextSession;

            if (session != null && session.IsOpen)
                throw new CacheException("You cannot register an interceptor once a session has already been opened");

            GetSession(interceptor);
        }

        public ISession GetSession()
        {
            return GetSession(null);
        }

        /// <summary>
        /// Gets a session with or without an interceptor.  This method is not called directly; instead,
        /// it gets invoked from other public methods.
        /// </summary>
        private ISession GetSession(IInterceptor interceptor)
        {
            ISession session = ContextSession;

            if (session == null || !session.IsOpen)
            {
                if (interceptor != null)
                {
                    session = sessionFactory.OpenSession(interceptor);
                }
                else
                {
                    session = sessionFactory.OpenSession();
                }

                ContextSession = session;
            }

            return session;
        }

        /// <summary>
        /// Flushes anything left in the session and closes the connection.
        /// </summary>
        public void CloseSession()
        {
            ISession session = ContextSession;

            if (session != null && session.IsOpen)
            {
                session.Close();
                ContextSession = null;
            }

            ContextSession = null;
        }

        public bool BeginTransaction()
        {

            ITransaction transaction = ContextTransaction;

            if (transaction == null)
            {
                transaction = GetSession().BeginTransaction();
                ContextTransaction = transaction;
                return true;
            }
            return false;
        }

        public void CommitTransaction()
        {
            ITransaction transaction = ContextTransaction;

            try
            {
                GetSession().Flush();
                if (HasOpenTransaction())
                {
                    transaction.Commit();
                    ContextTransaction = null;
                }
            }
            catch (HibernateException ex)
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                CloseSession();
            }
        }

        public bool HasOpenTransaction()
        {
            ITransaction transaction = ContextTransaction;

            return transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack;
        }

        public void RollbackTransaction()
        {
            ITransaction transaction = ContextTransaction;

            try
            {
                if (HasOpenTransaction())
                    transaction.Rollback();

                ContextTransaction = null;
            }
            finally
            {
                CloseSession();
            }
        }

        /// <summary>
        /// If within a web context, this uses <see cref="HttpContext" /> instead of the WinForms 
        /// specific <see cref="CallContext" />.  Discussion concerning this found at 
        /// http://forum.springframework.net/showthread.php?t=572.
        /// </summary>
        private ITransaction ContextTransaction
        {
            get
            {
                if (IsInWebContext())
                {
                    return (ITransaction)HttpContext.Current.Items[TRANSACTION_KEY];
                }
                else
                {
                    return (ITransaction)CallContext.GetData(TRANSACTION_KEY);
                }
            }
            set
            {
                if (IsInWebContext())
                {
                    HttpContext.Current.Items[TRANSACTION_KEY] = value;
                }
                else
                {
                    CallContext.SetData(TRANSACTION_KEY, value);
                }
            }
        }
        /// <summary>
        /// If within a web context, this uses <see cref="HttpContext" /> instead of the WinForms 
        /// specific <see cref="CallContext" />.  Discussion concerning this found at 
        /// http://forum.springframework.net/showthread.php?t=572.
        /// </summary>
        private ISession ContextSession
        {
            get
            {
                if (IsInWebContext())
                {
                    return (ISession)HttpContext.Current.Items[SESSION_KEY];
                }
                else
                {
                    return (ISession)CallContext.GetData(SESSION_KEY);
                }
            }
            set
            {
                if (IsInWebContext())
                {
                    HttpContext.Current.Items[SESSION_KEY] = value;
                }
                else
                {
                    CallContext.SetData(SESSION_KEY, value);
                }
            }
        }

        private bool IsInWebContext()
        {
            return HttpContext.Current != null;
        }

        private const string TRANSACTION_KEY = "CONTEXT_TRANSACTION";
        private const string SESSION_KEY = "CONTEXT_SESSION";
        private ISessionFactory sessionFactory;
    }
}