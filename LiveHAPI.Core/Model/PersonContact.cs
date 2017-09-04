using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class PersonContact : Entity<Guid>, IPersonContact
    {
        public int? Phone { get; set; }
        public bool Preferred { get; set; }
        
        public Guid PersonId { get; set; }

        public PersonContact()
        {
            Id = LiveGuid.NewGuid();
        }

        private PersonContact(int? phone, bool preferred, Guid personId):this()
        {
            Phone = phone;
            Preferred = preferred;
            PersonId = personId;
        }

        public static PersonContact Create(int? phone, bool preferred, Guid personId)
        {
            return new PersonContact(phone, preferred, personId);
        }
    } 
}