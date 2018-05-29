using System.Threading.Tasks;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.Setting;

namespace LiveHAPI.Core.Interfaces
{
    public interface IRestManager
    {
        Task<EmrFacility> VerfiyUrl(Endpoints dbProtocol);
        Endpoints ReadUrl(string connectionString);
    }
}