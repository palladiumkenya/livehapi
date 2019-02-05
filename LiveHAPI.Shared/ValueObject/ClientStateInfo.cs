using System;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ClientStateInfo: IClientState
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid? EncounterId { get; set; }
        public Guid? IndexClientId { get; set; }
        public LiveState Status { get; set; }
        public DateTime StatusDate { get; set; }
        public string StatusName => Status.ToString();
        public ClientStateInfo()
        {
        }

        public ClientStateInfo(Guid id,Guid clientId, Guid? encounterId, Guid? indexClientId, LiveState status, DateTime statusDate)
        {
            Id = id;
            ClientId = clientId;
            EncounterId = encounterId;
            IndexClientId = indexClientId;
            Status = status;
            StatusDate = statusDate;
        }
    }


  
 
}