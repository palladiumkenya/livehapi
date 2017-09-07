using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Device:Entity<Guid>
    {
        [MaxLength(50)]
        public string Serial { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        
        public Guid PracticeId { get; set; }

        public Device()
        {
            Id = LiveGuid.NewGuid();
        }

      
        public override string ToString()
        {
            return $"{Name} ({Serial})";
        }
    }
}