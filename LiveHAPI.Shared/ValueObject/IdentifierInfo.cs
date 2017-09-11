using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class IdentifierInfo: IEnrollment
    {
        public string IdentifierTypeId { get; set; }
        public string Identifier { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}