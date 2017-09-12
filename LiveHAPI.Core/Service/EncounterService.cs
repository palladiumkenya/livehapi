using System;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Service
{
    public class EncounterService:IEncounterService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPracticeRepository _practiceRepository;
        =

        public void Sync(EncounterInfo encounterInfo)
        {
            throw new NotImplementedException();
        }
    }
}