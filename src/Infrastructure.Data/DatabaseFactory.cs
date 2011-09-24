
using Infrastructure.CrossCutting;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using ISessionFactory = global::NHibernate.ISessionFactory;

namespace Infrastructure.Data
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private static ISessionFactory sessionFactory;
        private static ISession session;

        public DatabaseFactory()
        {
            sessionFactory = (new Configuration()).Configure().BuildSessionFactory();
        }

        public ISession Current()
        {
            if (session == null)
            {
                session = sessionFactory.OpenSession();
            }
            return session;
        }

        private void BuildSchema(Configuration config)
        {
            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config).Execute(false, true, false);
        }

    }
}
