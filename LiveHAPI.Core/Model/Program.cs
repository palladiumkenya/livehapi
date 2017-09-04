using System;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Program:Entity<Guid>
    {
        
        public Guid FormId { get; set; }
        
        public Guid EncounterTypeId { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }
        public Decimal Rank { get; set; }
    }
}