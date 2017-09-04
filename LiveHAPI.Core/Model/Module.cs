using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Module : Entity<Guid>
    {
        public string Name { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }
        public decimal Rank { get; set; }
        
        public List<Form> Forms { get; set; } = new List<Form>();

        public Module()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Display}";
        }
    }
}