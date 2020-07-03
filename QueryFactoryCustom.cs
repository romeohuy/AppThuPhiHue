using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SQLite;

namespace AppThuPhiHue
{
    public class QueryFactoryCustom : QueryFactory
    {
        public QueryFactory DbFactory;
        public QueryFactoryCustom()
        {
            // In real life you may read the configuration dynamically
            var connection = new SQLiteConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString
            );

            var compiler = new SqliteCompiler();

            DbFactory = new QueryFactory(connection, compiler);
        }
    }
}
