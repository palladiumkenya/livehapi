using System.Collections.Generic;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.ValueObject.Meta;

namespace LiveHAPI.Shared.ValueObject
{
    public class CountyInfo: ICounty
    {
        public  int Id { get; set; }
        public string Name { get; set; }

        public List<SubCountyInfo> SubCounties { get; set; }
    }
}