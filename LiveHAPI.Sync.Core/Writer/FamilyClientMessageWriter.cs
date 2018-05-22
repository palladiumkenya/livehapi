using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Enum;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Exchange.Messages;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Writers;
using Serilog;

namespace LiveHAPI.Sync.Core.Writer
{
    public class FamilyClientMessageWriter : ClientWriter<FamilyClientMessage>, IFamilyClientMessageWriter
    {
        public FamilyClientMessageWriter(IRestClient restClient, IFamilyClientMessageLoader loader,
            IClientStageRepository clientStageRepository) : base(restClient, loader, clientStageRepository)
        {
        }

        public override Task<IEnumerable<SynchronizeClientsResponse>> Write(params LoadAction[] actions)
        {
            return Write("api/Hts/family", actions);
        }
    }
}