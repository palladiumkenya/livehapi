using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class ClientAttribute:Entity<string>
    {
        public ClientAttribute()
        {
        }

        [MaxLength(100)]
        public string Name { get; set; }
        public Guid ClientId { get; set; }
    }
}