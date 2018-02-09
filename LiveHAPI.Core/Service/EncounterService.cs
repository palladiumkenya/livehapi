using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        private readonly IObsMemberScreeningRepository _obsMemberScreeningRepository;
        private readonly IObsFamilyTraceResultRepository _obsFamilyTraceResultRepository;
        private readonly IObsPartnerScreeningRepository _obsPartnerScreeningRepository;
        private readonly IObsPartnerTraceResultRepository _obsPartnerTraceResultRepository;
        

        public EncounterService(IClientRepository clientRepository, IPracticeRepository practiceRepository, IEncounterRepository encounterRepository, IObsRepository obsRepository, IObsFinalTestResultRepository obsFinalTestResultRepository, IObsLinkageRepository obsLinkageRepository, IObsTestResultRepository obsTestResultRepository, IObsTraceResultRepository obsTraceResultRepository, IObsMemberScreeningRepository obsMemberScreeningRepository, IObsFamilyTraceResultRepository obsFamilyTraceResultRepository, IObsPartnerScreeningRepository obsPartnerScreeningRepository, IObsPartnerTraceResultRepository obsPartnerTraceResultRepository)
        {
            _clientRepository = clientRepository;
            _practiceRepository = practiceRepository;
            _encounterRepository = encounterRepository;
            _obsRepository = obsRepository;
            _obsFinalTestResultRepository = obsFinalTestResultRepository;
            _obsLinkageRepository = obsLinkageRepository;
            _obsTestResultRepository = obsTestResultRepository;
            _obsTraceResultRepository = obsTraceResultRepository;
            _obsMemberScreeningRepository = obsMemberScreeningRepository;
            _obsFamilyTraceResultRepository = obsFamilyTraceResultRepository;
            _obsPartnerScreeningRepository = obsPartnerScreeningRepository;
            _obsPartnerTraceResultRepository = obsPartnerTraceResultRepository;
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

                        var bsLinkages = ObsLinkage.Create(encounterInfo);
                        _obsLinkageRepository.Insert(bsLinkages);
                        _obsLinkageRepository.Save();

                        var obsTraceResults = ObsTraceResult.Create(encounterInfo);
                        _obsTraceResultRepository.Insert(obsTraceResults);
                        _obsTraceResultRepository.Save();

                        var obsMemberScreening = ObsMemberScreening.Create(encounterInfo);
                        _obsMemberScreeningRepository.Insert(obsMemberScreening);
                        _obsMemberScreeningRepository.Save();

                        var obsFamilyTraceResults = ObsFamilyTraceResult.Create(encounterInfo);
                        _obsFamilyTraceResultRepository.Insert(obsFamilyTraceResults);
                        _obsFamilyTraceResultRepository.Save();
                        if (obsFamilyTraceResults.Count > 0)
                        {
                            var met = obsFamilyTraceResults.FirstOrDefault(x => null != x.Outcome && x.Outcome == new Guid("b25f9a81-852f-11e7-bb31-be2e44b06b34"));
                            if (null != met)
                            {
                                _obsFamilyTraceResultRepository.UpdateBooking(encounter, met);
                            }
                        }

                        var obsPartnerScreenings = ObsPartnerScreening.Create(encounterInfo);
                        _obsPartnerScreeningRepository.Insert(obsPartnerScreenings);
                        _obsPartnerScreeningRepository.Save();

                        var obsPartnerTraceResults = ObsPartnerTraceResult.Create(encounterInfo);
                        _obsPartnerTraceResultRepository.Insert(obsPartnerTraceResults);
                        _obsPartnerTraceResultRepository.Save();
                        if (obsPartnerTraceResults.Count > 0)
                        {
                            var met = obsPartnerTraceResults.FirstOrDefault(x => null != x.Outcome && x.Outcome == new Guid("b25f9a81-852f-11e7-bb31-be2e44b06b34"));
                            if (null != met)
                            {
                                _obsPartnerTraceResultRepository.UpdateBooking(encounter, met);
                            }
                        }

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

                        var bsLinkages = ObsLinkage.Create(encounterInfo);
                        _obsLinkageRepository.ReplaceAll(encounter.Id, bsLinkages);
                        _obsLinkageRepository.Save();

                        var obsTraceResults = ObsTraceResult.Create(encounterInfo);
                        _obsTraceResultRepository.ReplaceAll(encounter.Id, obsTraceResults);
                        _obsTraceResultRepository.Save();

                        var obsMemberScreening = ObsMemberScreening.Create(encounterInfo);
                        _obsMemberScreeningRepository.ReplaceAll(encounter.Id, obsMemberScreening);
                        _obsMemberScreeningRepository.Save();

                        var obsFamilyTraceResults = ObsFamilyTraceResult.Create(encounterInfo);
                        _obsFamilyTraceResultRepository.ReplaceAll(encounter.Id,obsFamilyTraceResults);
                        _obsFamilyTraceResultRepository.Save();
                        if (obsFamilyTraceResults.Count > 0)
                        {
                            var met = obsFamilyTraceResults.FirstOrDefault(x =>null != x.Outcome && x.Outcome == new Guid("b25f9a81-852f-11e7-bb31-be2e44b06b34"));
                            if (null != met)
                            {
                                _obsFamilyTraceResultRepository.UpdateBooking(encounter, met);
                            }
                        }

                        var obsPartnerScreenings = ObsPartnerScreening.Create(encounterInfo);
                        _obsPartnerScreeningRepository.ReplaceAll(encounter.Id, obsPartnerScreenings);
                        _obsPartnerScreeningRepository.Save();

                        var obsPartnerTraceResults = ObsPartnerTraceResult.Create(encounterInfo);
                        _obsPartnerTraceResultRepository.ReplaceAll(encounter.Id, obsPartnerTraceResults);
                        _obsPartnerTraceResultRepository.Save();
                        if (obsPartnerTraceResults.Count > 0)
                        {
                            var met = obsPartnerTraceResults.FirstOrDefault(x => null != x.Outcome && x.Outcome == new Guid("b25f9a81-852f-11e7-bb31-be2e44b06b34"));
                            if (null != met)
                            {
                                _obsPartnerTraceResultRepository.UpdateBooking(encounter, met);
                            }
                        }
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