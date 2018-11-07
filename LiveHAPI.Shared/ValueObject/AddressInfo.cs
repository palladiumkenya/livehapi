using System;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class AddressInfo:IAddress
    {
        public AddressInfo()
        {
        }

        public AddressInfo(Guid id, string landmark, int? countyId, decimal? lat, decimal? lng,Guid personId, int? subCountyId, int? wardId)
        {
            Id = id;
            Landmark = landmark;
            CountyId = countyId;
            Lat = lat;
            Lng = lng;
            PersonId = personId;
            SubCountyId = subCountyId;
            WardId = wardId;
        }

        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Landmark { get; set; }
        public int? CountyId { get; set; }
        public int? SubCountyId { get; set; }
        public int? WardId { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
    }
}