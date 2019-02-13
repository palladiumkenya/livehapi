using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Services;
using LiveHAPI.Sync.Core.Interface.Writers;
using LiveHAPI.Sync.Core.Interface.Writers.Index;

namespace LiveHAPI.Sync.Core.Service
{
    public class SyncClientsService : ISyncClientsService
    {
        private readonly IIndexClientMessageWriter _clientMessageWriter;
        private readonly IPartnerClientMessageWriter _partnerClientMessageWriter;
        private readonly IFamilyClientMessageWriter _familyClientMessageWriter;
        private readonly IDemographicsWriter _demographicsWriter;

        public SyncClientsService(IIndexClientMessageWriter clientMessageWriter, IPartnerClientMessageWriter partnerClientMessageWriter, IFamilyClientMessageWriter familyClientMessageWriter, IDemographicsWriter demographicsWriter)
        {
            _clientMessageWriter = clientMessageWriter;
            _partnerClientMessageWriter = partnerClientMessageWriter;
            _familyClientMessageWriter = familyClientMessageWriter;
            _demographicsWriter = demographicsWriter;
        }


        public async Task<int> Sync()
        {

            await _demographicsWriter.Write();
            
            //await _clientMessageWriter.Write();
            
            await _partnerClientMessageWriter.Write();
            
            await _familyClientMessageWriter.Write();
            
            return 1;
        }


        public void Dispose()
        {
            _demographicsWriter?.Dispose();
           // _clientMessageWriter?.Dispose();
            _partnerClientMessageWriter?.Dispose();
            _familyClientMessageWriter?.Dispose();
        }
    }
}