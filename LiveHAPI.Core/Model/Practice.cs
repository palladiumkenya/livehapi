using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    
    public class Practice : Entity<Guid>, IPractice
    {
        [MaxLength(20)]
        public string Code { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        [MaxLength(50)]
        public string PracticeTypeId { get; set; }
        public int? CountyId { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Provider> Providers { get; set; }
        public ICollection<Client> Clients { get; set; }

        public Practice()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Code} - {Name}";
        }
    }
}