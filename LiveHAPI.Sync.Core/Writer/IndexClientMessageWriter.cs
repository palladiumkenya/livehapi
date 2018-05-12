using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Sync.Core.Exchange;
using LiveHAPI.Sync.Core.Exchange.Messages;
using LiveHAPI.Sync.Core.Interface.Extractors;
using LiveHAPI.Sync.Core.Interface.Loaders;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Interface.Writers;
using Serilog;

namespace LiveHAPI.Sync.Core.Writer
{
    public class IndexClientMessageWriter : ClientWriter<IndexClientMessage>, IIndexClientMessageWriter
    {
        public IndexClientMessageWriter(IRestClient restClient, IIndexClientMessageLoader loader) : base(restClient, loader)
        {
        }

        public override Task<IEnumerable<SynchronizeClientsResponse>> Write()
        {
            return Write("api/Hts/indexclient");
        }
    }
}