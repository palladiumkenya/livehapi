using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class PersonAddress : Entity<Guid>, IAddress
    {
        [MaxLength(200)]
        public string Landmark { get; set; }
        public int? CountyId { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        [MaxLength(50)]
        public string Source { get; set; }
        [MaxLength(50)]
        public string SourceRef { get; set; }
        [MaxLength(50)]
        public string SourceSys { get; set; }

        public bool Preferred { get; set; }
        public Guid PersonId { get; set; }

        public PersonAddress()
        {
            Id = LiveGuid.NewGuid();
        }

        public void ChangeTo(PersonAddress address)
        {
            Landmark = address.Landmark;
            CountyId = address.CountyId;
            Lat = address.Lat;
            Lng = address.Lng;
        }
    }
}