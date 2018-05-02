using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Sync.Core.Interface;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Sync.Core.Reader
{
    public abstract class ClientReader<T>:IClientReader<T>
    {
        private readonly HttpClient _httpClient;
        protected ClientReader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual Task<IEnumerable<T>> Read()
        {
            return Read($"api/{typeof(T).Name}");
        }

        protected async Task<IEnumerable<T>> Read(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            var result = await response.Content.ReadAsJsonAsync<List<T>>();
            return result;
        }
    }
}