using System;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class AddressInfo:IAddress
    {
        public AddressInfo()
        {
        }

        public AddressInfo(Guid id, string landmark, int? countyId, decimal? lat, decimal? lng,Guid personId)
        {
            Id = id;
            Landmark = landmark;
            CountyId = countyId;
            Lat = lat;
            Lng = lng;
            PersonId = personId;
        }

        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Landmark { get; set; }
        public int? CountyId { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
    }
}