using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IClientService
    {
        IEnumerable<PersonMatch> SearchById(string searchItem);
        IEnumerable<PersonMatch> SearchByName(string searchItem);
        IEnumerable<Encounter> LoadEncounters(Guid id);
        void Sync(Guid practiceId, ClientInfo clients);
        void SyncClient(ClientInfo client);
    }
}