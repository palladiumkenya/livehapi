using System;

namespace LiveHAPI.Core.Interfaces.Model
{
    public interface IEnrollment
    {
        string IdentifierTypeId { get; set; }
        string Identifier { get; set; }
        DateTime RegistrationDate { get; set; }
    }
}