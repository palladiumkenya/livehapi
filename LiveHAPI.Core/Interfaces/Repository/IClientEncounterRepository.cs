using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.Subscriber;

namespace LiveHAPI.Core.Interfaces.Repository
{
    public interface IClientEncounterRepository : IRepository<Encounter, Guid>
    {
        IEnumerable<Encounter> GetReferralLinkage(Guid clientId);
        IEnumerable<Encounter> GetTracing(Guid clientId);
        IEnumerable<Encounter> GetTesting(Guid clientId);
        IEnumerable<Encounter> GetFinalTesting(Guid clientId);
        IEnumerable<Encounter> GetPretest(Guid clientId);
    }
}