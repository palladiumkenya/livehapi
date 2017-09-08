namespace LiveHAPI.Core.ValueModel
{
    public class DeviceLocation
    {
        public string IPAddress { get; set; }
        public decimal? Lng { get; set; }
        public decimal? Lat { get; set; }

        public DeviceLocation(string ipAddress, decimal? lng, decimal? lat)
        {
            IPAddress = ipAddress;
            Lng = lng;
            Lat = lat;
        }
    }
}