using System.Collections.Generic;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class County:Entity<int>
    {
        public string Name { get; set; }
        public ICollection<SubCounty> SubCounties { get; set; }=new List<SubCounty>();

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