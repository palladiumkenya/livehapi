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
        public bool Preferred { get; set; }
        
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public Guid PersonId { get; set; }

        public PersonAddress()
        {
            Id = LiveGuid.NewGuid();
        }

      
    }
}