using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject.Meta
{
    public class MasterFacilityInfo : IMasterFacility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AreaCode { get; set; }
        public string AreaInfo { get; set; }
    }
}