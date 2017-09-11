using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class PersonInfo:IPerson,IPersonName
    {
        public Guid Id { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? BirthDateEstimated { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MothersName { get; set; }
        public List<AddressInfo> Addresses { get; set; }
        public List<ContactInfo> Contacts { get; set; }

    }
}