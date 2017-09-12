using System;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IEncounterService
    {
        void Sync(EncounterInfo encounterInfo);
    }
}