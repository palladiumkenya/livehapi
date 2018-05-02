using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Model;

namespace LiveHAPI.Sync.Core.Reader
{
    public class ClientFacilityReader : ClientReader<ClientFacility>,IClientFacilityReader
    {
        public ClientFacilityReader(IRestClient restClient) : base(restClient)
        {
        }

        public override Task<IEnumerable<ClientFacility>> Read()
        {
            return Read("api/setup/getFacilities");
        }
    }
}