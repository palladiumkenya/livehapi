using LiveHAPI.Core.Model.Setting;

namespace LiveHAPI.Core.Interfaces
{
    public interface IDbManager
    {
        bool Verfiy(DbProtocol dbProtocol);
        string BuildConncetion(DbProtocol dbProtocol);
        DbProtocol ReadConnection(string connectionString);
    }
}