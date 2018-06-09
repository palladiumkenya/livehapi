using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IContactsEncounterRepository : IRepository<Encounter, Guid>
    {
        IEnumerable<Encounter> GetFamilyScreening(Guid? clientId=null);
        IEnumerable<Encounter> GetFamilyTracing(Guid? clientId = null);
        IEnumerable<Encounter> GetPartnerScreening(Guid? clientId = null);
        IEnumerable<Encounter> GetPartnerTracing(Guid? clientId = null);
    }
}