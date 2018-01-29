using System;

namespace LiveHAPI.Shared.ValueObject
{
    public class PersonDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool? BirthDateEstimated { get; set; }
       
    }
}