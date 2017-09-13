using System;
using LiveHAPI.Core.Model.Encounters;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IEncounterRepository : IRepository<Encounter,Guid>
    {
        
    }
}