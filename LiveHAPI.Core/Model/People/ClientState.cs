using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.People
{
    public class ClientState : Entity<Guid>, IClientState
    {
        public LiveState Status { get; set; }
        public DateTime StatusDate { get; set; }
        public Guid? EncounterId { get; set; }
        public Guid? IndexClientId { get; set; }
        public Guid ClientId { get; set; }

        public ClientState()
        {
            Id = LiveGuid.NewGuid();
        }

        public ClientState(Guid id, LiveState status, DateTime statusDate, Guid? encounterId, Guid? indexClientId):base(id)
        {
            Status = status;
            StatusDate = statusDate;
            EncounterId = encounterId;
            IndexClientId = indexClientId;
        }
        public static ClientState Create(ClientStateInfo state)
        {
            return new ClientState(state.Id, state.Status,state.StatusDate,state.EncounterId,state.IndexClientId);
        }
        public static List<ClientState> Create(ClientInfo clientInfo)
        {
            var list = new List<ClientState>();

            foreach (var stateInfo in clientInfo.ClientStates)
            {
                list.Add(Create(stateInfo));
            }
            return list;
        }

        public static List<ClientStateInfo> GetClientStateInfos(List<ClientState> states)
        {
            var list = new List<ClientStateInfo>();
            foreach (var state in states)
            {
                list.Add(state.GetClientStateInfo());
            }

            return list;
        }

        public ClientStateInfo GetClientStateInfo()
        {
            return new ClientStateInfo(Id,ClientId,EncounterId,IndexClientId,Status,StatusDate);
        }

        public override string ToString()
        {
            return $"{Status}|{StatusDate:F}";
        }
    }
}