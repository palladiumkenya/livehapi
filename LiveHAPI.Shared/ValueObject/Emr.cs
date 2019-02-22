using System;

namespace LiveHAPI.Shared.ValueObject
{
    public class Emr
    {
        public string AppVer { get; set; }
        public string DbVer { get; set; }
        public DateTime? RelDate { get; set; }
        public string VersionName { get; set; }
    }
}