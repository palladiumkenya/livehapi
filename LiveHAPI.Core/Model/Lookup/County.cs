using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Lookup
{
    public class County:Entity<int>, ICounty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public  ICollection<SubCounty> SubCounties { get; set; }=new List<SubCounty>();
        public  ICollection<Practice> Practices { get; set; } = new List<Practice>();
        public ICollection<PersonAddress> PersonAddresses { get; set; } = new List<PersonAddress>();
        public County()
        {
        }

        public County(int id, string name) : base(id)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}