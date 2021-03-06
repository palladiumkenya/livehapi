﻿using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.ValueObject;
using Serilog;

namespace LiveHAPI.Core.Service
{
    public class ActivationService:IActivationService
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IPracticeActivationRepository _practiceActivationRepository;
        private readonly IMasterFacilityRepository _masterFacilityRepository;

        public ActivationService(IPracticeRepository practiceRepository, IPracticeActivationRepository practiceActivationRepository, IMasterFacilityRepository masterFacilityRepository)
        {
            _practiceRepository = practiceRepository;
            _practiceActivationRepository = practiceActivationRepository;
            _masterFacilityRepository = masterFacilityRepository;
        }

        public Practice GetCentral()
        {
            int code = 1;
            var existingPractice = _practiceRepository.GetByCode(code.ToString());

            if (null == existingPractice)
                throw new ArgumentException("Central hAPI.Server does not exist");

            return existingPractice;
        }

        public Practice GetLocal()
        {
            var existingPractice =
                _practiceRepository.GetAll(x => x.IsDefault && x.Code != "1").ToList().FirstOrDefault();

            if (null == existingPractice)
                throw new ArgumentException("Facility hAPI.Server not setup");

            return existingPractice;
        }

        public MasterFacility Verify(int code)
        {
            var facility = _masterFacilityRepository.Get(code);

            if (null == facility)
                throw new ArgumentException("Facility does not exist");



            return facility;
        }



        public Practice EnrollPractice(string code)
        {
            var existingPractice = _practiceRepository.GetByCode(code);

            if (null != existingPractice)
                return existingPractice;

            var facility = Verify(Convert.ToInt32(code));

            if (null != facility)
            {
                var practice = Practice.CreateFacility(facility);
                _practiceRepository.InsertOrUpdate(practice);
                _practiceRepository.Save();
                return practice;
            }
            return null;
        }

        public void EnrollDevicePractice(List<Practice> practices)
        {
            foreach (var practice in practices)
            {
                var existingPractice = _practiceRepository.Get(practice.Id);

                if (null == existingPractice)
                {
                    Log.Debug($"Enrolling new practice {practice}");
                    practice.MakeFacility();
                    _practiceRepository.InsertOrUpdate(practice);
                    _practiceRepository.Save();

                }
            }
        }

        public string EnrollDevice(DeviceInfo info)
        {
            var activation = _practiceActivationRepository.GetAll(x => x.Device.IsSameAs(info.Serial)).FirstOrDefault();
            if (null == activation)
            {
                var practiceActivation = PracticeActivation.Create(info);
                _practiceActivationRepository.Insert(practiceActivation);
                _practiceRepository.Save();
                return practiceActivation.IdentifierPrefix;
            }

            return activation.IdentifierPrefix;
        }

        public string GetActivationCode(string code, DeviceInfo info, DeviceLocationInfo locationInfo=null)
        {
            
            var practice = _practiceRepository.GetByCode(code);

            if (null == practice)
                throw new ArgumentException("Facility Code Not found");

            var activation= practice.ActivateDevice(info, locationInfo);

            if(null==activation)
                throw new ArgumentException("Actrivation not possible");

            _practiceActivationRepository.InsertOrUpdate(activation);
            _practiceRepository.Save();
            return activation.ActivationCode;
        }
    }
}