using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.People
{
    public class Client : Entity<Guid>, IClient
    {
        [MaxLength(50)]
        public string MaritalStatus { get; set; }

        [MaxLength(50)]
        public string KeyPop { get; set; }

        [MaxLength(100)]
        public string OtherKeyPop { get; set; }

        public Guid PracticeId { get; set; }
        public Guid PersonId { get; set; }
        public ICollection<ClientIdentifier> Identifiers { get; set; } = new List<ClientIdentifier>();
        public ICollection<ClientRelationship> Relationships { get; set; } = new List<ClientRelationship>();
        public ICollection<ClientAttribute> Attributes { get; set; } = new List<ClientAttribute>();
        public ICollection<Encounter> Encounters { get; set; } = new List<Encounter>();

        public Client()
        {
            Id = LiveGuid.NewGuid();
        }

        private Client(string maritalStatus, string keyPop, string otherKeyPop, Guid practiceId, Guid personId)
        {
            MaritalStatus = maritalStatus;
            KeyPop = keyPop;
            OtherKeyPop = otherKeyPop;
            PracticeId = practiceId;
            PersonId = personId;
        }

        public static Client Create(ClientInfo clientInfo, Guid practiceId, Guid personId)
        {
            var client = new Client(clientInfo.MaritalStatus, clientInfo.KeyPop, clientInfo.OtherKeyPop, practiceId,
                personId);

            var identifiers = ClientIdentifier.Create(clientInfo);
            client.AddIdentifiers(identifiers);

            var relationships = ClientRelationship.Create(clientInfo);
            client.AddRelationships(relationships);

            return client;
        }
        public void Update(ClientInfo clientInfo)
        {
            MaritalStatus = clientInfo.MaritalStatus;
            KeyPop = clientInfo.KeyPop;
            OtherKeyPop = clientInfo.OtherKeyPop;

            Identifiers.Clear();
            var identifiers = ClientIdentifier.Create(clientInfo);
            AddIdentifiers(identifiers);

            Relationships.Clear();
            var relationships = ClientRelationship.Create(clientInfo);
            AddRelationships(relationships);
        }

        private void AddIdentifier(ClientIdentifier personName)
        {
            personName.ClientId = Id;
            Identifiers.Add(personName);
        }
        private void AddIdentifiers(List<ClientIdentifier> personNames)
        {
            foreach (var personName in personNames)
            {
                AddIdentifier(personName);
            }
        }

        private void AddRelationship(ClientRelationship personName)
        {
            personName.ClientId = Id;
            Relationships.Add(personName);
        }
        private void AddRelationships(List<ClientRelationship> personNames)
        {
            foreach (var personName in personNames)
            {
                AddRelationship(personName);
            }
        }

      
    }
}