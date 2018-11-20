using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    [ComplexType]
    public class Contact
    {
        public Guid Id { get; set; }
        public string Serial { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Sex { get; set; }
        public string Relation { get; set; }

        public Contact()
        {
        }

        private Contact(Guid id, string serial, string firstName, string middleName, string lastName,
            DateTime dateOfBirth, int sex, string relation)
        {
            Id = id;
            Serial = serial;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Sex = sex;
            Relation = relation;
        }
        public static Contact CreatePrimary(ClientStage client)
        {
            return Create(client, null);
        }
        public static Contact CreateSecondary(ClientStage client, ClientStageRelationship relationship)
        {
            return Create(client, relationship);
        }

        private static Contact Create(ClientStage client, ClientStageRelationship relationship)
        {
            return new Contact
            {
                Id = client.Id,
                Serial = client.Serial,
                FirstName = client.FirstName,
                MiddleName = client.MiddleName,
                LastName = client.LastName,
                DateOfBirth = client.DateOfBirth,
                Sex = client.Sex,
                Relation = null != relationship ? relationship.RelationName : string.Empty
            };
        }
        
        public override string ToString()
        {
            return $"{Serial} {FirstName} {LastName}";

        }
    }
}