using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Client:Entity<Guid>, IProfile
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

        public Client()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}