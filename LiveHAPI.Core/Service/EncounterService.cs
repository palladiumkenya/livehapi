using System;
using System.Collections.Generic;
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
        private readonly IObsFinalTestResultRepository _obsFinalTestResultRepository;
        private readonly IObsLinkageRepository _obsLinkageRepository;
        private readonly IObsTestResultRepository _obsTestResultRepository;
        private readonly IObsTraceResultRepository _obsTraceResultRepository;
        

        public EncounterService(IClientRepository clientRepository, IPracticeRepository practiceRepository, IEncounterRepository encounterRepository, IObsRepository obsRepository, IObsFinalTestResultRepository obsFinalTestResultRepository, IObsLinkageRepository obsLinkageRepository, IObsTestResultRepository obsTestResultRepository, IObsTraceResultRepository obsTraceResultRepository)
        {
            _clientRepository = clientRepository;
            _practiceRepository = practiceRepository;
            _encounterRepository = encounterRepository;
            _obsRepository = obsRepository;
            _obsFinalTestResultRepository = obsFinalTestResultRepository;
            _obsLinkageRepository = obsLinkageRepository;
            _obsTestResultRepository = obsTestResultRepository;
            _obsTraceResultRepository = obsTraceResultRepository;
        }

        public void Sync(List<EncounterInfo> encounterInfos)
        {
            foreach (var encounterInfo in encounterInfos)
            {
                //Check client
                var client = _clientRepository.Get(encounterInfo.ClientId);
                if (null != client)
                {
                    var encounter = _encounterRepository.Get(encounterInfo.Id);

                    if (null == encounter)
                    {
                        encounter = Encounter.Create(encounterInfo);
                        _encounterRepository.Insert(encounter);
                        _encounterRepository.Save();
                        
                        var obs = Obs.Create(encounterInfo);
                        _obsRepository.Insert(obs);
                        _obsRepository.Save();

                        var obsTestResult = ObsTestResult.Create(encounterInfo);
                        _obsTestResultRepository.Insert(obsTestResult);
                        _obsTestResultRepository.Save();

                        var obsFinalTestResults = ObsFinalTestResult.Create(encounterInfo);
                        _obsFinalTestResultRepository.Insert(obsFinalTestResults);
                        _obsFinalTestResultRepository.Save();

                        var obsTraceResults = ObsTraceResult.Create(encounterInfo);
                        _obsTraceResultRepository.Insert(obsTraceResults);
                        _obsTraceResultRepository.Save();

                        var bsLinkages = ObsLinkage.Create(encounterInfo);
                        _obsLinkageRepository.Insert(bsLinkages);
                        _obsLinkageRepository.Save();
                    }
                    else
                    {
                        encounter.Update(encounterInfo);
                        _encounterRepository.Update(encounter);
                        _encounterRepository.Save();

                        var obs = Obs.Create(encounterInfo);
                        _obsRepository.ReplaceAll(encounter.Id, obs);
                        _obsRepository.Save();

                        var obsTestResult = ObsTestResult.Create(encounterInfo);
                        _obsTestResultRepository.ReplaceAll(encounter.Id,obsTestResult);
                        _obsTestResultRepository.Save();

                        var obsFinalTestResults = ObsFinalTestResult.Create(encounterInfo);
                        _obsFinalTestResultRepository.ReplaceAll(encounter.Id, obsFinalTestResults);
                        _obsFinalTestResultRepository.Save();

                        var obsTraceResults = ObsTraceResult.Create(encounterInfo);
                        _obsTraceResultRepository.ReplaceAll(encounter.Id, obsTraceResults);
                        _obsTraceResultRepository.Save();

                        var bsLinkages = ObsLinkage.Create(encounterInfo);
                        _obsLinkageRepository.ReplaceAll(encounter.Id, bsLinkages);
                        _obsLinkageRepository.Save();

                    }
                }
            }
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