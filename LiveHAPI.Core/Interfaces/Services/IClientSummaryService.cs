using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.People;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IClientSummaryService
    {
        IEnumerable<ClientSummary> Generate(Client client);
    }
}