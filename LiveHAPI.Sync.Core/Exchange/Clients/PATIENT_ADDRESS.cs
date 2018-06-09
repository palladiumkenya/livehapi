namespace LiveHAPI.Sync.Core.Exchange.Clients
{
    public class PATIENT_ADDRESS
    {
        public PHYSICAL_ADDRESS PHYSICAL_ADDRESS { get; set; }
        public string POSTAL_ADDRESS { get; set; }

        public PATIENT_ADDRESS()
        {
        }

        private PATIENT_ADDRESS(PHYSICAL_ADDRESS physicalAddress)
        {
            PHYSICAL_ADDRESS = physicalAddress;
        }

        public static PATIENT_ADDRESS Create(string landmark)
        {
            return new PATIENT_ADDRESS(PHYSICAL_ADDRESS.Create(landmark));
        }
    }
}