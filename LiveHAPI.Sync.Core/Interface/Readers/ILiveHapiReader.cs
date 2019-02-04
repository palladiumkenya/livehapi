using System;
using System.Threading.Tasks;
using LiveHAPI.Shared.ValueObject;
using LiveHAPI.Sync.Core.Model;

namespace LiveHAPI.Sync.Core.Interface.Readers
{
    public interface ILiveHapiReader:IDisposable
    {
        Task<HapiSettingsView> ReadHapi();
    }
}