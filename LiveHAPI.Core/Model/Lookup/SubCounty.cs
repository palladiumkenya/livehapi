using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Lookup
{
    public class SubCounty:Entity<Guid>, ISubCounty
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int CountyId { get; set; }
        
        public SubCounty()
        {
            Id = LiveGuid.NewGuid();
        }
    }
}