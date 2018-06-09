using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;

namespace LiveHAPI.Sync.Core.Exchange
{
    public class HtsRegistry
    {
        public MESSAGE_HEADER MESSAGE_HEADER { get; set; }
        public List<NEWCLIENT> CLIENTS { get; set; }

        public HtsRegistry()
        {
        }

        private HtsRegistry(MESSAGE_HEADER messageHeader, List<NEWCLIENT> clients)
        {
            MESSAGE_HEADER = messageHeader;
            CLIENTS = clients;
        }

        public static HtsRegistry Create(string facility,List<ClientStage> clients)
        {
            var newclients=new List<NEWCLIENT>();

            foreach (var client in clients)
            {
                newclients.Add(NEWCLIENT.Create(client));
            }
            return new HtsRegistry(MESSAGE_HEADER.Create(facility), newclients);
        }

        public static HtsRegistry Create(string facility, ClientStage clientStage, ENCOUNTERS encounters)
        {
            var newclients = new List<NEWCLIENT> {NEWCLIENT.Create(clientStage, encounters)};
            return new HtsRegistry(MESSAGE_HEADER.Create(facility), newclients);
        }
    }
}