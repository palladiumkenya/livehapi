using System;
using LiveHAPI.Core.ValueModel;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IActivationService
    {
        string GetActivationCode(string code, DeviceIdentity identity, DeviceLocation location=null);
    }
}