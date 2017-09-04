using System;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Practice : Entity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        
        public string PracticeTypeId { get; set; }
        
        public int? CountyId { get; set; }

        public override string ToString()
        {
            return $"{Code} - {Name}";
        }
    }
}