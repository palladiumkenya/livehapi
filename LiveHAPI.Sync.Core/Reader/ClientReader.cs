using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Sync.Core.Interface.Readers;
using Serilog;

namespace LiveHAPI.Sync.Core.Reader
{
    public abstract class ClientReader<T>:IClientReader<T>
    {
        protected readonly HttpClient _httpClient;
        protected ClientReader(IRestClient restClient)
        {
            _httpClient = restClient.Client;
        }

        public virtual Task<IEnumerable<T>> Read()
        {
            return Read($"api/{typeof(T).Name}");
        }

        protected async Task<IEnumerable<T>> Read(string endpoint)
        {
            var result=new List<T>();
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                var data=await response.Content.ReadAsJsonAsync<List<T>>();
                result = data.ToList();
            }
            catch (Exception e)
            {
                Log.Error($"error reading endpint [{endpoint}] for {typeof(T).Name}");
                Log.Error($"{e}");
            }
            
            return result;
        }
    }
}