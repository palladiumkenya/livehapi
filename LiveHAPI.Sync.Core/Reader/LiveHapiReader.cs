using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.ValueObject;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Model;
using Serilog;

namespace LiveHAPI.Sync.Core.Reader
{
    public class LiveHapiReader : ILiveHapiReader
    {
        private readonly IRestClient _restClient;
        
        public LiveHapiReader(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<HapiSettingsView> ReadHapi()
        {
            var result=new HapiSettingsView();
            try
            {
                var response = await _restClient.Client.GetAsync("api/sync/hapi");
                var data=await response.Content.ReadAsJsonAsync<HapiSettingsView>();
                result = data;
            }
            catch (Exception e)
            {
                Log.Error($"error reading endpint [api/sync/hapi]");
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