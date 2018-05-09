using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Exchange
{
    public class ClientPretestDisabilityStage : Entity<Guid>
    {
        public int Disabilities { get; set; }
        public Guid ClientPretestStageId { get; set; }
    }
}