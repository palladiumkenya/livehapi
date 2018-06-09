using System.Threading.Tasks;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Sync.Core.Interface
{
    public interface IStartup
    {
       Task<HapiSettingsView> LoadSettings();
    }
}