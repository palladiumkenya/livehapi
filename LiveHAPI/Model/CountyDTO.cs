using System.Collections.Generic;
using LiveHAPI.Core.Model.Lookup;

namespace LiveHAPI.Model
{
    public class CountyDTO
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public List<SubCounty> SubCounties { get; set; }=new List<SubCounty>();
    }
}