using System.Data;
using System.Data.SqlClient;
using LiveHAPI.Core.Interfaces;
using LiveHAPI.Core.Model.Setting;

namespace LiveHAPI.Infrastructure
{
    public class DbManager:IDbManager
    {
        public bool Verfiy(DbProtocol dbProtocol)
        {
            bool isConnected = false;

            var sb = new SqlConnectionStringBuilder();
            sb.DataSource = dbProtocol.Server;
            sb.UserID = dbProtocol.User;
            sb.Password = dbProtocol.Password;
            sb.InitialCatalog = dbProtocol.Database;
            sb.MultipleActiveResultSets = true;
            sb.PersistSecurityInfo = true;
            sb.Pooling = true;

            using (var cn=new SqlConnection(sb.ConnectionString))
            {
                cn.Open();
                isConnected = cn.State == ConnectionState.Open;
            }

            return isConnected;
        }

        public string BuildConncetion(DbProtocol dbProtocol)
        {
            var sb = new SqlConnectionStringBuilder();
            sb.DataSource = dbProtocol.Server;
            sb.UserID = dbProtocol.User;
            sb.Password = dbProtocol.Password;
            sb.InitialCatalog = dbProtocol.Database;
            sb.MultipleActiveResultSets = true;
            sb.Pooling = true;
            sb.PersistSecurityInfo = true;
            return sb.ConnectionString;
        }

        public DbProtocol ReadConnection(string connectionString)
        {
            var sb=new SqlConnectionStringBuilder(connectionString);

            return new DbProtocol(sb.DataSource,sb.InitialCatalog,sb.UserID,sb.Password);
        }
    }
}