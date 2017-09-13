using System;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Service
{
    public class EncounterService:IEncounterService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPracticeRepository _practiceRepository;
        private readonly IEncounterRepository _encounterRepository;
        private readonly IObsRepository _obsRepository;

        public EncounterService(IClientRepository clientRepository, IPracticeRepository practiceRepository, IEncounterRepository encounterRepository, IObsRepository obsRepository)
        {
            _clientRepository = clientRepository;
            _practiceRepository = practiceRepository;
            _encounterRepository = encounterRepository;
            _obsRepository = obsRepository;
        }

        public void Sync(EncounterInfo encounterInfo)
        {
            //Check pracitce
            var practice = _practiceRepository.Get(encounterInfo.PracticeId);
            if (null==practice)
                throw new ArgumentException("Facility is not registered");


            //Check client
            var client = _clientRepository.Get(encounterInfo.ClientId);
            if (null == client)
            throw new ArgumentException("Client is not registered");

            var encounter = _encounterRepository.Get(encounterInfo.Id);

            if (null == encounter)
            {
                encounter = Encounter.Create(encounterInfo);
                _encounterRepository.Insert(encounter);
                _encounterRepository.Save();


                var obs = Obs.Create(encounterInfo);
                _obsRepository.Insert(obs);
                _obsRepository.Save();
            }
            else
            {
                encounter.Update(encounterInfo);
                _encounterRepository.Update(encounter);
                _encounterRepository.Save();

                var obs = Obs.Create(encounterInfo);
                _obsRepository.ReplaceAll(encounter.Id, obs);
                _obsRepository.Save();
            }
        }
    }
}