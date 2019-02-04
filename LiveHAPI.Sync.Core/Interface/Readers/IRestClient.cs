using System;
using System.Net.Http;

namespace LiveHAPI.Sync.Core.Interface.Readers
{
    public interface IRestClient:IDisposable
    {
        HttpClient Client { get; }
    }
}

