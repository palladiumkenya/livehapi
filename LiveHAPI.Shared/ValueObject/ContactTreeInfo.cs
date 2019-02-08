using System.Collections.Generic;

namespace LiveHAPI.Shared.ValueObject
{
    public class ContactTreeInfo
    {
        public bool IsPrimary { get; set; }
        public string Serial { get; set; }
        public string Label { get; set; }
        public List<ContactTreeInfo> Children { get; set; } = new List<ContactTreeInfo>();
    }
}