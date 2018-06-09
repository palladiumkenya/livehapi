using System.Net.Http;

namespace LiveHAPI.Sync.Core.Interface.Readers
{
    public interface IRestClient
    {
        HttpClient Client { get; }
    }
}

