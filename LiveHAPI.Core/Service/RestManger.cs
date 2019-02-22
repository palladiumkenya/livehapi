using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.Setting;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.ValueObject;
using Serilog;

namespace LiveHAPI.Core.Service
{
    public class RestManger:IRestManager
    {
        private readonly string fac = "api/setup/getFacilities";
        private readonly string emrEndpoint = "api/setup/iqcareversion";
        public async Task<EmrFacility> VerfiyUrl(Endpoints endpoints)
        {
            var http = new HttpClient {BaseAddress = new Uri(endpoints.Iqcare.HasToEndWith(@"/"))};
            try
            {
                var response = await http.GetAsync(fac);
                var data=await response.Content.ReadAsJsonAsync<List<EmrFacility>>();
                var emrFacilities= data.ToList();
                if (emrFacilities.Any())
                {
                    var facility = emrFacilities.FirstOrDefault(x => x.Preferred.HasValue && x.Preferred.Value == 1);
                    return null == facility ? emrFacilities.First() : facility;
                }
            }
            catch (Exception e)
            {
                Log.Error($"error reading endpint [{fac}]");
                Log.Error($"{e}");
            }

            return null;
        }

        public async Task<Emr> ReadEmr(Endpoints endpoints)
        {
            var http = new HttpClient {BaseAddress = new Uri(endpoints.Iqcare.HasToEndWith(@"/"))};
            try
            {
                var response = await http.GetAsync(emrEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsJsonAsync<List<Emr>>();
                    return data.FirstOrDefault();
                }
                throw new Exception("Please update IQCare to latest version");
            }
            catch (Exception e)
            {
                Log.Error($"error reading endpoint [{emrEndpoint}]");
                Log.Error($"{e}");
                if (e.Message.Contains("Please update IQCare to latest version"))
                    throw;
            }

            return null;
        }

        public async Task<bool> VerfiyUrl(string url)
        {
            var fac = await VerfiyUrl(new Endpoints(url));
            return null != fac;
        }

        public Endpoints ReadUrl(string endpoints)
        {
            return new Endpoints(endpoints);
        }
    }
}
