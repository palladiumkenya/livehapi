using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IClientService
    {
        IEnumerable<PersonMatch> SearchById(string searchItem);
        IEnumerable<PersonMatch> SearchByName(string searchItem);
        void Sync(Guid practiceId, ClientInfo clients);
        void SyncClient(ClientInfo client);
    }
}