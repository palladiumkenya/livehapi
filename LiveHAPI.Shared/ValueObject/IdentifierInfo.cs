using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class IdentifierInfo: IEnrollment
    {
        public Guid Id { get; set; }
        public string IdentifierTypeId { get; set; }
        public string Identifier { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Guid ClientId { get; set; }

        public IdentifierInfo()
        {
        }

        public IdentifierInfo(Guid id, string identifierTypeId, string identifier, DateTime registrationDate, Guid clientId)
        {
            Id = id;
            IdentifierTypeId = identifierTypeId;
            Identifier = identifier;
            RegistrationDate = registrationDate;
            ClientId = clientId;
        }

        
    }
}