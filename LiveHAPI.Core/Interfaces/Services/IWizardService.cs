using LiveHAPI.Core.Model.Setting;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IWizardService
    {
        Endpoints ReadEndpoints();
        ConnectionStrings ReadConnectionStrings();
        ConnectionStrings ReadSyncConnectionStrings();
        bool UpdateDatabase(string connectionString,DbProtocol dbprotocol);
        bool UpdateSyncUrl(RestProtocol dbprotocol);
        bool ValidateDatabase();
        bool ValidateUrl();
    }
}