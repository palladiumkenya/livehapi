using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class ClientIdentifier : Entity<Guid>,IEnrollment
    {
        [MaxLength(50)]
        public string IdentifierTypeId { get; set; }
        [MaxLength(100)]
        public string Identifier { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Preferred { get; set; }
        public Guid ClientId { get; set; }

        public ClientIdentifier()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{IdentifierTypeId}|{Identifier}";
        }
    }
}