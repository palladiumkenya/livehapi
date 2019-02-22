using System.Threading.Tasks;
using LiveHAPI.Sync.Core.Interface.Services;
using LiveHAPI.Sync.Core.Interface.Writers;
using LiveHAPI.Sync.Core.Interface.Writers.Family;
using LiveHAPI.Sync.Core.Interface.Writers.Index;
using LiveHAPI.Sync.Core.Interface.Writers.Partner;

namespace LiveHAPI.Sync.Core.Service
{
    public class LegacySyncClientsService : ISyncClientsService
    {
        private readonly IIndexClientMessageWriter _clientMessageWriter;
        private readonly IPartnerWriter _partnerWriter;
        private readonly IFamilyWriter _familyWriter;

        private readonly IPartnerClientMessageWriter _partnerClientMessageWriter;
        private readonly IFamilyClientMessageWriter _familyClientMessageWriter;
        private readonly IDemographicsWriter _demographicsWriter;

        public LegacySyncClientsService(IIndexClientMessageWriter clientMessageWriter, IPartnerClientMessageWriter partnerClientMessageWriter, IFamilyClientMessageWriter familyClientMessageWriter, IDemographicsWriter demographicsWriter, IPartnerWriter partnerWriter, IFamilyWriter familyWriter)
        {
            _clientMessageWriter = clientMessageWriter;
            _partnerClientMessageWriter = partnerClientMessageWriter;
            _familyClientMessageWriter = familyClientMessageWriter;
            _demographicsWriter = demographicsWriter;
            _partnerWriter = partnerWriter;
            _familyWriter = familyWriter;
        }


        public async Task<int> Sync()
        {

            //await _demographicsWriter.Write();
            await _clientMessageWriter.Write();

            //await _partnerWriter.Write();
            await _partnerClientMessageWriter.Write();

            //await _familyWriter.Write();
            await _familyClientMessageWriter.Write();

            return 1;
        }


        public void Dispose()
        {
            //_demographicsWriter?.Dispose();
            _clientMessageWriter?.Dispose();
            //_partnerWriter?.Dispose();
            _partnerClientMessageWriter?.Dispose();
            //_familyWriter?.Dispose();
            _familyClientMessageWriter?.Dispose();
        }
    }
}
