using System;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject.Meta
{
    public class SubCountyInfo: ISubCounty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CountyId { get; set; }
    
    }
}