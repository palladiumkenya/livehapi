using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Client:Entity<Guid>, IProfile
    {
        
        public IEnumerable<ClientIdentifier> Identifiers { get; set; } = new List<ClientIdentifier>();
        
        public IEnumerable<ClientRelationship> Relationships { get; set; } = new List<ClientRelationship>();
        public string MaritalStatus { get; set; }
        public string KeyPop { get; set; }
        public string OtherKeyPop { get; set; }
        
        public Guid PracticeId { get; set; }
        
        public Guid PersonId { get; set; }
        
        public Person Person { get; set; }

        public Client()
        {
            Id = LiveGuid.NewGuid();
        }

        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId):this()
        {
            MaritalStatus = maritalStatus;
            KeyPop = keyPop;
            OtherKeyPop = otherKeyPop;
            PracticeId = practiceId;
        }

        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Guid personId)
            :this(maritalStatus, keyPop, otherKeyPop,practiceId)
        {
            PersonId = personId;
        }
        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Person person)
            : this(maritalStatus, keyPop, otherKeyPop, practiceId)
        {
            Person = person;
        }

        public static Client Create(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Person person)
        {
            return new Client(maritalStatus, keyPop, otherKeyPop,practiceId,person);
        }
        public static Client CreateFromPerson(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId,  Guid personId)
        {
            return new Client(maritalStatus, keyPop, otherKeyPop, practiceId, personId);
        }

        public override string ToString()
        {
            return $"{Person} ,{Person.Gender}";
        }
    }
}