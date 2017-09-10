using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class PersonContact : Entity<Guid>, IContact
    {
        public int Phone { get; set; }
        [MaxLength(50)]
        public string Source { get; set; }
        [MaxLength(50)]
        public string SourceRef { get; set; }
        [MaxLength(50)]
        public string SourceSys { get; set; }
        public bool Preferred { get; set; }
        public Guid PersonId { get; set; }
        public PersonContact()
        {
            Id = LiveGuid.NewGuid();
        }

        public void ChangeTo(PersonContact contact)
        {
            Phone = contact.Phone;
        }
    } 
}