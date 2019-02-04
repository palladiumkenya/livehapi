using System;
using System.Net.Http;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Sync.Core.Interface.Readers;

namespace LiveHAPI.Sync.Core.Reader
{
    public class RestClient : IRestClient
    {
        public HttpClient Client { get; }

        public RestClient(string baseUrl)
        {
            Client = new HttpClient {BaseAddress = new Uri(baseUrl.HasToEndWith(@"/"))};
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Client?.Dispose();
            }
        }
    }
}