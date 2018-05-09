using System;
using LiveHAPI.Sync.Core.Exchange;

namespace LiveHAPI.Sync.Core.Interface.Loaders
{
    public interface IHtsEncounterLoader : ILoader<ENCOUNTERS>
    {
        ENCOUNTERS Load(Guid clientId);
    }
}