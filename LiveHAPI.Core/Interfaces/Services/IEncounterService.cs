using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IEncounterService
    {
        void Sync(List<EncounterInfo> encounterInfos);
        void Sync(EncounterInfo encounterInfo);
    }
}