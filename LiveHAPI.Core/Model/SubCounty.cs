using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class SubCounty:Entity<Guid>
    {
        public string Name { get; set; }
        
        public int CountyId { get; set; }

        public SubCounty()
        {
            Id = LiveGuid.NewGuid();
        }

        public SubCounty(string name, int countyId) : this()
        {
            Name = name;
            CountyId = countyId;
        }
    }
}