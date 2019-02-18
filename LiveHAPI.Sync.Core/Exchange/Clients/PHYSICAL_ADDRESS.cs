using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Sync.Core.Exchange.Clients
{
    public class PHYSICAL_ADDRESS
    {
        public string VILLAGE { get; set; }
        public string WARD { get; set; }
        public string SUB_COUNTY { get; set; }
        public string COUNTY { get; set; }
        public string LANDMARK { get; set; }
        public string GPS_LOCATION { get; set; }

        public PHYSICAL_ADDRESS()
        {
        }

        private PHYSICAL_ADDRESS(string landmark,string county, string subCounty, string ward)
        {
            LANDMARK = landmark;
            COUNTY = county;
            SUB_COUNTY = subCounty;
            WARD = ward;
        }

        public static PHYSICAL_ADDRESS Create(string landmark, int? county, int? subCounty, int? ward)
        {
            return new PHYSICAL_ADDRESS(landmark,county.ToIqLocation("0"),subCounty.ToIqLocation("0"),ward.ToIqLocation("0"));
        }
    }
}
