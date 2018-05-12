using System;
using System.Collections.Generic;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using System.Linq;
using System.Text.RegularExpressions;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Dapper;
using LiveHAPI.Core.Model.Exchange;
using Z.Dapper.Plus;

namespace LiveHAPI.Infrastructure.Repository
{
    public class ClientStageRepository : BaseRepository<ClientStage, Guid>, IClientStageRepository
    {
        public ClientStageRepository(LiveHAPIContext context) : base(context)
        {
        }
        public void Clear()
        {
            using (var con = GetDbConnection())
            {
                con.Execute($"DELETE FROM {nameof(ClientStage)}s");
            }
        }

        public void BulkInsert(IEnumerable<ClientStage> clientStages)
        {
            using (var con = GetDbConnection())
            {
                con.BulkInsert(clientStages);
            }
        }

        public IEnumerable<ClientStage> GetIndexClients()
        {
            return GetAll(x => x.IsIndex);
        }
    }
}