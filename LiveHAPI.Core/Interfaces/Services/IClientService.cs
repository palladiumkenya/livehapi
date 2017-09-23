using System;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IClientService
    {
        void Sync(Guid practiceId, ClientInfo clients);
        void SyncClient(ClientInfo client);
    }
}