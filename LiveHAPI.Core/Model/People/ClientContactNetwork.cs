using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class ClientContactNetwork:Entity<Guid>
    {
        public Guid ClientId { get; set; }
        public string Serial { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Sex { get; set; }
        public string Relation { get; set; }
        public Guid? ClientContactNetworkId { get; set; }
        public ICollection<ClientContactNetwork> Networks { get; set; }=new List<ClientContactNetwork>();
        public DateTime Generated { get; set; }
        [NotMapped] public bool IsPrimary => Networks.Any();

        public ClientContactNetwork()
        {
            Id = LiveGuid.NewGuid();
            Generated=DateTime.Now;
        }
        
        private ClientContactNetwork(Guid clientId, string serial, string firstName, string middleName, string lastName,
            DateTime dateOfBirth, int sex, string relation,Guid? clientContactNetworkId):this()
        {
            ClientId =clientId;
            Serial = serial;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Sex = sex;
            Relation = relation;
            ClientContactNetworkId = clientContactNetworkId;
        }

        public static ClientContactNetwork CreatePrimary (Contact contact)
        {
            return Create(contact, null);
        }
        public static ClientContactNetwork CreateSecondary (Contact contact,Guid? clientContactNetworkId )
        {
            return Create(contact, clientContactNetworkId);
        }

        public static ClientContactNetwork Create (Contact contact,Guid? clientContactNetworkId )
        {
            return new ClientContactNetwork(
                contact.Id,
                contact.Serial,
                contact.FirstName,
                contact.MiddleName,
                contact.LastName,
                contact.DateOfBirth,
                contact.Sex,
                contact.Relation,
                clientContactNetworkId);
        }
        
       
        public override string ToString()
        {
            return $"{Serial} {FirstName} {LastName} ({Generated:yyyy MMMM dd})- {ClientContactNetworkId}";
        }
    }
}