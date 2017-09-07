using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Core.Model.Studio;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Network
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
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Provider> Providers { get; set; } = new List<Provider>();
        public ICollection<Client> Clients { get; set; } = new List<Client>();
        public ICollection<PracticeActivation> Activations { get; set; }=new List<PracticeActivation>();
        public ICollection<FormImplementation> FormImplementation { get; set; } = new List<FormImplementation>();
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