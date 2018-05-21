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
    public class ClientPretestStageRepository : BaseRepository<ClientPretestStage, Guid>, IClientPretestStageRepository
    {
        public ClientPretestStageRepository(LiveHAPIContext context) : base(context)
        {
        }

        public override IEnumerable<ClientPretestStage> GetAll(bool voided = false)
        {
            return DbSet.Include(x => x.Disabilities).AsNoTracking();
        }

        public IEnumerable<ClientPretestStage> GetByClientId(Guid clientId)
        {
            return DbSet.Where(x=>x.ClientId==clientId).Include(x => x.Disabilities).AsNoTracking();
        }

        public void Clear()
        {
            using (var con = GetDbConnection())
            {
                con.Execute($"DELETE FROM {nameof(ClientPretestStage)}s");
            }
        }

        public void BulkInsert(IEnumerable<ClientPretestStage> clientStages)
        {
            using (var con = GetDbConnection())
            {
                con.BulkInsert(clientStages);
                if (clientStages.Any())
                {
                    var clientPretestDisabilityStages = clientStages.SelectMany(x => x.Disabilities);
                    if (clientPretestDisabilityStages.Any())
                        con.BulkInsert(clientPretestDisabilityStages);
                }
            }
        }
    }
}