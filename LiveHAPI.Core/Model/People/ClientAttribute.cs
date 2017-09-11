using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class ClientAttribute:Entity<string>, IClientAttribute
    {
        [Key]
        [MaxLength(50)]
        public override string Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        public Guid ClientId { get; set; }
    }
}