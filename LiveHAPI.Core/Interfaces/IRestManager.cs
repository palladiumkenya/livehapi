using System.Threading.Tasks;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.Setting;

namespace LiveHAPI.Core.Interfaces
{
    public interface IRestManager
    {
        Task<EmrFacility> VerfiyUrl(Endpoints dbProtocol);
        Task<bool> VerfiyUrl(string url);
        Endpoints ReadUrl(string connectionString);
    }
}