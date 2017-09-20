using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ClientInfo:IClient
    {
        public Guid Id { get; set; }
        public string MaritalStatus { get; set; }
        public string KeyPop { get; set; }
        public string OtherKeyPop { get; set; }
        public Guid? PracticeId { get; set; }
        public string PracticeCode { get; set; }
        public PersonInfo Person { get; set; }
        public List<IdentifierInfo> Identifiers { get; set; }=new List<IdentifierInfo>();
        public List<RelationshipInfo> Relationship { get; set; } = new List<RelationshipInfo>();

        /*
         client.Person.FirstName,
                client.Person.MiddleName,
                client.Person.LastName,
                GetSex(client.Person.Gender),
                client.Person.BirthDate.Value,
                GetDobPrecion(client.Person.BirthDateEstimated.Value),
                client.Identifiers.First().Identifier,
                locationId,
                client.Identifiers.First().RegistrationDate,
                client.Id,
                client.Person.Addresses.FirstOrDefault().Landmark,
                client.Person.Contacts.FirstOrDefault().Phone.ToString()
         */
    }
}