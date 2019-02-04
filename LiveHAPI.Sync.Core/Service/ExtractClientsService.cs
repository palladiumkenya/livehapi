using System.Threading.Tasks;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Services;
using LiveHAPI.Sync.Core.Interface.Writers;
using Serilog;

namespace LiveHAPI.Sync.Core.Service
{
    public class ExtractClientsService : IExtractClientsService
    {
        private readonly IClientStageExtractor _clientStageExtractor;
        private readonly IClientStageRelationshipExtractor _clientStageRelationshipExtractor;
        private readonly IClientPretestStageExtractor _clientPretestStageExtractor;

        public ExtractClientsService(IClientStageExtractor clientStageExtractor, IClientStageRelationshipExtractor clientStageRelationshipExtractor, IClientPretestStageExtractor clientPretestStageExtractor)
        {
            _clientStageExtractor = clientStageExtractor;
            _clientStageRelationshipExtractor = clientStageRelationshipExtractor;
            _clientPretestStageExtractor = clientPretestStageExtractor;
        }


        public async Task<int> Sync()
        {
            
            await _clientStageExtractor.ExtractAndStage();
            
            await _clientStageRelationshipExtractor.ExtractAndStage();
            
            await _clientPretestStageExtractor.ExtractAndStage();
            
            return 1;
        }

        public void Dispose()
        {
            Log.Debug("disposing....");
            _clientStageExtractor?.Dispose();
            _clientStageRelationshipExtractor?.Dispose();
            _clientPretestStageExtractor?.Dispose();
        }
    }
}