using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class SubCounty:Entity<Guid>
    {
        [Required(ErrorMessage = "A valid subcounty code is required")]
        [Range(1,Int32.MaxValue, ErrorMessage = "A valid subcounty code is required")]
        public int Code { get; set; }
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required]
        public int CountyId { get; set; }

        public SubCounty()
        {
            Id = LiveGuid.NewGuid();
        }

        public SubCounty(string name, int code, int countyId) : this()
        {
            Name = name;
            CountyId = countyId;
            Code = code;
        }
    }
}