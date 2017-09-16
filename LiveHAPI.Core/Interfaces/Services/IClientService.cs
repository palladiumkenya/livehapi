using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IClientService
    {
        void Sync(Guid practiceId, ClientInfo clients);
        void Sync(List<ClientInfo>  clients);
    }
}