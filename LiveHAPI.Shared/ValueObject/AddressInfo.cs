using System;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class AddressInfo:IAddress
    {
        public string Landmark { get; set; }
        public int? CountyId { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
    }
}