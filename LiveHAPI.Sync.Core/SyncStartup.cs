using System;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.ValueObject;
using LiveHAPI.Sync.Core.Interface;
using Serilog;

namespace LiveHAPI.Sync.Core
{
    public class SyncStartup:IStartup
    {
        private readonly string _url;
        private readonly HttpClient _client;
        public SyncStartup(string url)
        {
            _url = url;
            _client=new HttpClient{BaseAddress = new Uri(url.HasToEndWith(@"/"))};
        }

        public async Task<HapiSettingsView> LoadSettings()
        {
            try
            {
                var response = await _client.GetAsync("api/sync/hapi");
                response.EnsureSuccessStatusCode();
                var data=await response.Content.ReadAsJsonAsync<HapiSettingsView>();
                return data;
            }
            catch (Exception e)
            {
                Log.Error(e,$"error reading hapi endpint [api/sync/hapi]");
            }

            return null;
        }
    }
}