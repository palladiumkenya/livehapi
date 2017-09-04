using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class ClientIdentifier : Entity<Guid>,IEnrollment
    {
        
        public string IdentifierTypeId { get; set; }
        public string Identifier { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Preferred { get; set; }
        
        public Guid ClientId { get; set; }

        public ClientIdentifier()
        {
            Id = LiveGuid.NewGuid();
        }

        private ClientIdentifier(string identifierTypeId, string identifier, DateTime registrationDate, bool preferred, Guid clientId):this()
        {
            IdentifierTypeId = identifierTypeId;
            Identifier = identifier;
            RegistrationDate = registrationDate;
            Preferred = preferred;
            ClientId = clientId;
        }

        public static ClientIdentifier Create(string identifierTypeId, string identifier, DateTime registrationDate,bool preferred, Guid clientId)
        {
            return new ClientIdentifier(identifierTypeId, identifier, registrationDate, preferred, clientId);
        }

        public override string ToString()
        {
            return $"{IdentifierTypeId}|{Identifier}";
        }
    }
}