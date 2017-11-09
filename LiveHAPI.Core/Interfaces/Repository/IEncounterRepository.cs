using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IEncounterRepository : IRepository<Encounter,Guid>
    {
        List<Encounter> LoadEncounters(Guid clientId);
    }
}