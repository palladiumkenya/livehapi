using System.Threading.Tasks;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.Setting;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Interfaces
{
    public interface IRestManager
    {
        Task<EmrFacility> VerfiyUrl(Endpoints dbProtocol);
        Task<Emr> ReadEmr(Endpoints dbProtocol);
        Task<bool> VerfiyUrl(string url);
        Endpoints ReadUrl(string connectionString);
    }
}
