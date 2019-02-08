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
        private readonly IRestClient _restClient;
        protected ClientReader(IRestClient restClient)
        {
            _restClient = restClient;
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
                var response = await _restClient.Client.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsJsonAsync<List<T>>();
                    result = data.ToList();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
                
            }
            catch (Exception e)
            {
                Log.Error($"error reading endpint [{endpoint}] for {typeof(T).Name}");
                Log.Error($"{e}");
            }
            
            return result;
        }

        public void Dispose()
        {
            _restClient?.Dispose();
        }
    }
}