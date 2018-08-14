using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHAPI.Core.Model.Exchange;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface ISyncManagerService
    {
        IEnumerable<ClientStage> GetSyncErrorClients();
        int GetSyncErrorClientsCount();
        Task Resend(IEnumerable<Guid> clientIds);
        Task ResendAll();
        Stats GetStats();
    }
}