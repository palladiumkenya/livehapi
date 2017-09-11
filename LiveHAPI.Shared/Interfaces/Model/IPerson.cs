using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IPerson
    {
        string Gender { get; set; }
        DateTime? BirthDate { get; set; }
        bool? BirthDateEstimated { get; set; }
    }
}