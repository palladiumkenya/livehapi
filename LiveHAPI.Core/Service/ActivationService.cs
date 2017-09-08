using System;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.ValueModel;

namespace LiveHAPI.Core.Service
{
    public class ActivationService:IActivationService
    {
        public string GetActivationCode(string code, DeviceIdentity identity, DeviceLocation location)
        {
            throw new NotImplementedException();
        }
    }
}