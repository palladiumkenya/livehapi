using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Model;

namespace LiveHAPI.Sync.Core.Reader
{
    public class ClientLookupReader : ClientReader<ClientLookup>,IClientLookupReader
    {
        public ClientLookupReader(IRestClient restClient) : base(restClient)
        {
        }

        public override Task<IEnumerable<ClientLookup>> Read()
        {
            return Read("api/setup/htsOptions");
        }
    }
}