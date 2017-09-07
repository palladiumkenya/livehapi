using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Program:Entity<Guid>
    {
        
        public Guid FormId { get; set; }
        
        public Guid EncounterTypeId { get; set; }
        [MaxLength(50)]
        public string Display { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public Decimal Rank { get; set; }

        public Program()
        {
            Id=Guid.NewGuid();
        }
    }
}