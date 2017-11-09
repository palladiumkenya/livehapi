using System;
using System.Collections.Generic;
using System.Text;

namespace LiveHAPI.Shared.ValueObject
{
    public class RemoteClientInfo
    {
        public ClientInfo Client { get; set; }
        public List<EncounterInfo> Encounters { get; set; }=new List<EncounterInfo>();
    }
}
