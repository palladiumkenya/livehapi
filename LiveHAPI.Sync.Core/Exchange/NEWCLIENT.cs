using System.Collections.Generic;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Sync.Core.Exchange.Clients;

namespace LiveHAPI.Sync.Core.Exchange
{
    public class NEWCLIENT
    {
        public PATIENT_IDENTIFICATION PATIENT_IDENTIFICATION { get; set; }
        public ENCOUNTERS ENCOUNTER { get; set; }

        public NEWCLIENT()
        {
        }

        private NEWCLIENT(PATIENT_IDENTIFICATION patientIdentification)
        {
            PATIENT_IDENTIFICATION = patientIdentification;
        }
        private NEWCLIENT(PATIENT_IDENTIFICATION patientIdentification, ENCOUNTERS encounter)
        {
            PATIENT_IDENTIFICATION = patientIdentification;
            ENCOUNTER = encounter;
        }

        public static NEWCLIENT Create(ClientStage  client, ENCOUNTERS encounter)
        {
            return new NEWCLIENT(PATIENT_IDENTIFICATION.Create(client),encounter );
        }

        public static NEWCLIENT Create(ClientStage client)
        {
            return new NEWCLIENT(PATIENT_IDENTIFICATION.Create(client));
        }
    }
}