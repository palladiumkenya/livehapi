using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class County:Entity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        public string Name { get; set; }
        public virtual ICollection<SubCounty> SubCounties { get; set; }=new List<SubCounty>();

        public County()
        {
        }

        public County(int id, string name) : base(id)
        {
            Name = name;
        }

        public void AddSubCounty(SubCounty subCounty)
        {
            subCounty.CountyId = Id;
            this.SubCounties.Add(subCounty);
        }
    }
}