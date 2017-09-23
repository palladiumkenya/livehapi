namespace LiveHAPI.Shared.ValueObject
{
    public class DeviceLocationInfo
    {
        public string IPAddress { get; set; }
        public decimal? Lng { get; set; }
        public decimal? Lat { get; set; }

        public DeviceLocationInfo(string ipAddress, decimal? lng, decimal? lat)
        {
            IPAddress = ipAddress;
            Lng = lng;
            Lat = lat;
        }
    }
}