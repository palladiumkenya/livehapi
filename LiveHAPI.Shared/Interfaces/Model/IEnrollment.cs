using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IEnrollment
    {
        string IdentifierTypeId { get; set; }
        string Identifier { get; set; }
        DateTime RegistrationDate { get; set; }
    }
}