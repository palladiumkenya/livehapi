using System.Data.Common;
using LiveHAPI.Core.Interfaces;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Setting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace LiveHAPI.Core.Service
{
    public class WizardService:IWizardService
    {
        
        private readonly IConfiguration _hapiConfiguration;
        private readonly IConfiguration _syncConfiguration;
        private readonly IDbManager _dbManager;

        public WizardService(IConfiguration hapiConfiguration, IConfiguration syncConfiguration, IDbManager dbManager)
        {
            _hapiConfiguration = hapiConfiguration;
            _syncConfiguration = syncConfiguration;
            _dbManager = dbManager;
        }

        public Endpoints ReadEndpoints()
        {
            return new Endpoints(_syncConfiguration.GetSection("endpoints:iqcare").Value);
        }

        public ConnectionStrings ReadConnectionStrings()
        {
            return new ConnectionStrings(
                _hapiConfiguration.GetConnectionString("hAPIConnection"),
                _hapiConfiguration.GetConnectionString("EMRConnection")
                );
        }

        public ConnectionStrings ReadSyncConnectionStrings()
        {
            return new ConnectionStrings(
                _syncConfiguration.GetConnectionString("hAPIConnection"),
                _syncConfiguration.GetConnectionString("EMRConnection")
            );
        }

        public bool UpdateDatabase(string connectionString, DbProtocol dbprotocol)
        {
           // var updatedConncetion = _dbManager.GetConncetion(connectionString, dbprotocol);
            // var sb = new SqlConnectionStringBuilder();
            throw new System.NotImplementedException();
        }

        public bool UpdateDatabase(DbProtocol dbprotocol)
        {
            var emrConn = ReadConnectionStrings().EmrConnection;
          //  var connectionString = _dbManager.GetConncetion(emrConn, dbprotocol);
           // var sb = new SqlConnectionStringBuilder();
            throw new System.NotImplementedException();
        }

        public bool UpdateSyncUrl(RestProtocol dbprotocol)
        {
            throw new System.NotImplementedException();
        }

        public bool ValidateDatabase()
        {
            throw new System.NotImplementedException();
        }

        public bool ValidateUrl()
        {
            throw new System.NotImplementedException();
        }
    }
}