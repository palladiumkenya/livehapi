using System;
using System.Collections.Generic;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Dapper;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Shared.Enum;
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

        public void Clear(Guid clientId)
        {
            string sql = $@"
                            DELETE
                                FROM {nameof(ClientStage)}s
                            WHERE
                                {nameof(ClientStage.ClientId)} = @ClientId;
                          ";

            using (var con = GetDbConnection())
            {
                con.Execute(sql,new {ClientId = clientId});
            }
        }

        public void BulkInsert(IEnumerable<ClientStage> clientStages)
        {
            using (var con = GetDbConnection())
            {
                con.BulkInsert(clientStages);
            }
        }

        public void BulkUpdate(IEnumerable<ClientStage> clientStages)
        {
            using (var con = GetDbConnection())
            {
                con.BulkUpdate(clientStages);
            }
        }


        public IEnumerable<ClientStage> GetIndexClients()
        {
            return GetAll(x => x.IsIndex && x.SyncStatus == SyncStatus.Staged);
        }

        public IEnumerable<ClientStage> GetByStatus(SyncStatus status)
        {
            return GetAll(x => x.SyncStatus == status);
        }

        public IEnumerable<ClientStage> GetByStatusGeneric(SyncStatus status)
        {
            var selSql = $@"
                SELECT        c.Id, c.DateOfBirth, c.DateOfBirthPrecision, c.FirstName, c.KeyPop, c.Landmark, c.LastName, c.MaritalStatus, c.MiddleName, c.Phone, c.Serial, c.Sex, c.StatusDate, c.SyncStatus, c.SyncStatusInfo, c.Voided, c.RegistrationDate,
                              c.ClientId, c.IsIndex, c.UserId, c.PracticeId, c.SiteCode, c.NickName, c.County, c.SubCounty, c.Ward, c.Completion, c.Education, c.Occupation, u.UserName, u.Id AS LiveUserId
                FROM            ClientStages AS c LEFT OUTER JOIN
                                Users AS u ON c.UserId = u.SourceRef";

            var sql = $@"{selSql} WHERE c.SyncStatus={(int)status}";

            var results = ExecQuery<dynamic>(sql).ToList();
            return Mapper.Map<IEnumerable<ClientStage>>(results);
        }

        public ClientStage GetQueued(Guid clientId)
        {
            return DbSet.AsNoTracking()
                .FirstOrDefault(x => x.ClientId == clientId && x.SyncStatus == SyncStatus.Staged);
        }

        public void UpdateSyncStatus(Guid clientId, SyncStatus syncStatus, string statusInfo="")
        {
            string sql = $@"
                            UPDATE {nameof(ClientStage)}s
                            SET
                                {nameof(ClientStage.SyncStatus)} = @SyncStatus,
                                {nameof(ClientStage.SyncStatusInfo)} = @SyncStatusInfo,
                                {nameof(ClientStage.StatusDate)}=@StatusDate
                            WHERE
                                {nameof(ClientStage.ClientId)} = @ClientId;
                          ";

            using (var con = GetDbConnection())
            {
                con.Execute(sql,new {ClientId = clientId, SyncStatus =syncStatus, SyncStatusInfo=statusInfo, StatusDate=DateTime.Now});
            }
        }

        public async Task UpdateSyncStatus(IEnumerable<Guid> clientIds, SyncStatus syncStatus, string statusInfo = "")
        {
            var backLog = new List<KeyValuePair<Guid, string>>();

            foreach (var clientId in clientIds)
            {
                string sql = $@"
                            UPDATE {nameof(ClientStage)}s
                            SET
                                {nameof(ClientStage.SyncStatus)} = @SyncStatus,
                                {nameof(ClientStage.SyncStatusInfo)} = @SyncStatusInfo,
                                {nameof(ClientStage.StatusDate)}=@StatusDate
                            WHERE
                                {nameof(ClientStage.ClientId)} = @ClientId;
                          ";
                backLog.Add(new KeyValuePair<Guid, string>(clientId, sql));
            }

            using (var con = GetDbConnection())
            {
                foreach (var backLogItem in backLog)
                {
                    await con.ExecuteAsync(backLogItem.Value,
                        new
                        {
                            ClientId = backLogItem.Key,
                            SyncStatus = syncStatus,
                            SyncStatusInfo = statusInfo,
                            StatusDate = DateTime.Now
                        });
                }
            }
        }

        public async  Task UpdateAllWithSyncStatus(SyncStatus syncStatus, SyncStatus newSyncStatus, string statusInfo = "")
        {
            var allWith = GetByStatus(syncStatus).ToList();

            var ids = allWith.Select(x => x.ClientId).ToList();
            if (ids.Any())
            {
                await UpdateSyncStatus(ids, newSyncStatus, statusInfo);
            }
        }

        public bool ClientExisits(Guid clientId)
        {
            return DbSet
                .AsNoTracking()
                .Any(x => x.ClientId == clientId);
        }
    }
}
