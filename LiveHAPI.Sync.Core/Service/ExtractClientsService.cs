using System.Threading.Tasks;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Services;
using LiveHAPI.Sync.Core.Interface.Writers;

namespace LiveHAPI.Sync.Core.Service
{
    public class ExtractClientsService : IExtractClientsService
    {
        private readonly IClientStageExtractor _clientStageExtractor;
        private readonly IClientStageRelationshipExtractor _clientStageRelationshipExtractor;
        private readonly IClientPretestStageExtractor _clientPretestStageExtractor;
        
        
        private readonly IIndexClientMessageWriter _clientMessageWriter;
        private readonly IPartnerClientMessageWriter _partnerClientMessageWriter;
        private readonly IFamilyClientMessageWriter _familyClientMessageWriter;

        public ExtractClientsService(IIndexClientMessageWriter clientMessageWriter, IPartnerClientMessageWriter partnerClientMessageWriter, IFamilyClientMessageWriter familyClientMessageWriter)
        {
            _clientMessageWriter = clientMessageWriter;
            _partnerClientMessageWriter = partnerClientMessageWriter;
            _familyClientMessageWriter = familyClientMessageWriter;
        }


        public async Task<int> Sync()
        {
            
            await _clientMessageWriter.Write();
            
            await _partnerClientMessageWriter.Write();
            
            await _familyClientMessageWriter.Write();
            
            return 1;
        }
    }
}