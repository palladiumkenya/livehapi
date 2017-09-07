using System;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class PersonContact : Entity<Guid>, IContact
    {
        public int? Phone { get; set; }
        public bool Preferred { get; set; }
        
        public Guid PersonId { get; set; }

        public PersonContact()
        {
            Id = LiveGuid.NewGuid();
        }

       
    } 
}