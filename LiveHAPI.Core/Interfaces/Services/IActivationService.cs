using System;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.ValueModel;

namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IActivationService
    {
        MasterFacility Verify(int code);
        Practice EnrollPractice(string code);
        string GetActivationCode(string code, DeviceIdentity identity, DeviceLocation location=null);
    }
}