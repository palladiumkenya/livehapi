using System;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Device:Entity<Guid>
    {
        public string Serial { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        
        public Guid PracticeId { get; set; }

        public Device()
        {
        }

        public Device(string serial, string code, string name, Guid practiceId)
        {
            Serial = serial;
            Code = code;
            Name = name;
            PracticeId = practiceId;
        }

        public override string ToString()
        {
            return $"{Name} ({Serial})";
        }
    }
}