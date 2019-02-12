using System.Collections.Generic;
using LiveHAPI.Sync.Core.Exchange.Clients;

namespace LiveHAPI.Sync.Core.Exchange
{
    public class NEWAFYAMOBILECLIENT
    {
        public PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
       
        public NEWAFYAMOBILECLIENT()
        {
        }

        private NEWAFYAMOBILECLIENT(PATIENT_IDENTIFICATION patientIdentification)
        {
            PATIENT_IDENTIFICATION = patientIdentification;
        }

        public static NEWAFYAMOBILECLIENT Create(NEWCLIENT newclient)
        {
            return new NEWAFYAMOBILECLIENT(newclient.PATIENT_IDENTIFICATION);
        }
        
        public static List<NEWAFYAMOBILECLIENT> Create(List<NEWCLIENT> newclients)
        {
            var clients=new List<NEWAFYAMOBILECLIENT>();
            
            foreach (var newclient in newclients)
            {
                clients.Add(Create(newclient));
            }

            return clients;
        }
    }
}