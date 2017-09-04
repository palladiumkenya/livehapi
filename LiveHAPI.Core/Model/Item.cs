using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Item : Entity<Guid>
    {
        public string Code { get; set; }
        public string Display { get; set; }

        public Item()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}