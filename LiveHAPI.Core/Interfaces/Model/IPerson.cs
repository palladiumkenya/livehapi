using System;

namespace LiveHAPI.Core.Interfaces.Model
{
    public interface IPerson
    {
        string Gender { get; set; }
        DateTime? BirthDate { get; set; }
        bool? BirthDateEstimated { get; set; }
    }
}