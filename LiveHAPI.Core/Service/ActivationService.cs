using System;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.ValueModel;

namespace LiveHAPI.Core.Service
{
    public class ActivationService:IActivationService
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IPracticeActivationRepository _practiceActivationRepository;

        public ActivationService(IPracticeRepository practiceRepository, IPracticeActivationRepository practiceActivationRepository)
        {
            _practiceRepository = practiceRepository;
            _practiceActivationRepository = practiceActivationRepository;
        }

        public MasterFacility Verify(int code)
        {
            throw new NotImplementedException();
        }

        public string GetActivationCode(string code, DeviceIdentity identity, DeviceLocation location=null)
        {
            var practice = _practiceRepository.GetByCode(code);

            if (null == practice)
                throw new ArgumentException("Facility Code Not found");

            var activation= practice.ActivateDevice(identity, location);

            if(null==activation)
                throw new ArgumentException("Actrivation not possible");

            _practiceActivationRepository.InsertOrUpdate(activation);

            return activation.ActivationCode;
        }
    }
}