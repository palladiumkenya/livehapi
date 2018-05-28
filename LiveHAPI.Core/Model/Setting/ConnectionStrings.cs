namespace LiveHAPI.Core.Model.Setting
{
    public class ConnectionStrings
    {
        public string HapiConnection { get; set; }
        public string EmrConnection { get; set; }

        public ConnectionStrings()
        {
        }

        public ConnectionStrings(string hapiConnection, string emrConnection)
        {
            HapiConnection = hapiConnection;
            EmrConnection = emrConnection;
        }
    }
}