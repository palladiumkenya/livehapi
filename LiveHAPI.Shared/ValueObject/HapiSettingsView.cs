using System.Security.Cryptography;

namespace LiveHAPI.Shared.ValueObject
{
    public class HapiSettingsView
    {
        public string Connection { get; set; }
        public string Url { get; set; }
        public bool IsVerifed { get; set; }
        public int SyncVersion { get; set; }
        public override string ToString()
        {
            return $"{nameof(Connection)}:{Connection}\n{nameof(Url)}:{Url}\n{nameof(IsVerifed)}:[{IsVerifed}";
        }
    }
}
