using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Exchange
{
    public class ClientPretestDisabilityStage : Entity<Guid>
    {
        public int Disabilities { get; set; }
        public Guid ClientPretestStageId { get; set; }

        public ClientPretestDisabilityStage()
        {
            Id = LiveGuid.NewGuid();
        }

        public ClientPretestDisabilityStage(int disability, Guid clientPretestStageId):this()
        {
            Disabilities = disability;
            ClientPretestStageId = clientPretestStageId;
        }

       

        public override string ToString()
        {
            return $" [{Disabilities} {Id}]";
        }
    }
}