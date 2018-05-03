namespace LiveHAPI.Sync.Core.Model
{
    public class ClientLookup
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string DisplayName { get; set; }
        public string MasterName { get; set; }
        public override string ToString()
        {
            return $"{ItemName} - {DisplayName} ({MasterName})";
        }
    }
}