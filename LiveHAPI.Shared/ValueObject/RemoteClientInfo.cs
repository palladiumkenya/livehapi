using System;
using System.Collections.Generic;
using System.Text;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.Shared.ValueObject
{
    public class RemoteClientInfo
    {
        public ClientInfo Client { get; set; }
        public List<EncounterInfo> Encounters { get; set; }=new List<EncounterInfo>();

        public bool HasPractice => !Client.PracticeId.IsNullOrEmpty();

        public RemoteClientInfo()
        {
            Client=new ClientInfo();
        }
    }
}
