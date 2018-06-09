namespace LiveHAPI.Sync.Core.Exchange.Clients
{
    public class PHYSICAL_ADDRESS
    {
        public string VILLAGE { get; set; }
        public string WARD { get; set; }
        public string SUB_COUNTY { get; set; }
        public string COUNTY { get; set; }
        public string LANDMARK { get; set; }
        public string GPS_LOCATION { get; set; }

        public PHYSICAL_ADDRESS()
        {
        }

        private PHYSICAL_ADDRESS(string landmark)
        {
            LANDMARK = landmark;
        }

        public static PHYSICAL_ADDRESS Create(string landmark)
        {
            return new PHYSICAL_ADDRESS(landmark);
        }
    }
}