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

namespace LiveHAPI.Sync.Core.Service
{
    public class SyncClientsService : ISyncClientsService
    {
        private readonly IIndexClientMessageWriter _clientMessageWriter;
        private readonly IPartnerClientMessageWriter _partnerClientMessageWriter;
        private readonly IFamilyClientMessageWriter _familyClientMessageWriter;

        public SyncClientsService(IIndexClientMessageWriter clientMessageWriter, IPartnerClientMessageWriter partnerClientMessageWriter, IFamilyClientMessageWriter familyClientMessageWriter)
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