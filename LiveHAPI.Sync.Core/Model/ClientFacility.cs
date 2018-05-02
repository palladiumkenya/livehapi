namespace LiveHAPI.Sync.Core.Model
{
    public class ClientFacility
    {
        public int FacilityID { get; set; }
        public string FacilityName { get; set; }
        public string PosID { get; set; }
        public int Preferred { get; set; }
        public int DeleteFlag { get; set; }

        public override string ToString()
        {
            return $"{PosID} - {FacilityName}";
        }
    }
}