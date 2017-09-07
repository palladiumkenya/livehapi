using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Item : Entity<Guid>
    {
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string Display { get; set; }

        public Item()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}