using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using EFCore.BulkExtensions;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using LiveHAPI.IQCare.Core.Model;
using LiveHAPI.Shared;
using LiveHAPI.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;
using Dapper;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.Custom;
using Serilog;


namespace LiveHAPI.IQCare.Infrastructure.Repository
{
    public class PSmartStoreRepository : BaseRepository, IPSmartStoreRepository
    {
        private List<SqlAction> _sqlActions;

        public PSmartStoreRepository(EMRContext context) : base(context)
        {
        }

        public void CreateOrUpdate(List<PSmartStoreInfo> storeInfos, SubscriberSystem subscriberSystem, Location location)
        {
            var sql5 = GenerateSqlActionsPsmart(storeInfos, subscriberSystem, location);

            using (SqlConnection conn = new SqlConnection(Context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql5, conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Log.Error($"{e}");
                        throw;
                    }
                }
            }
        }
        private string GenerateSqlActionsPsmart(List<PSmartStoreInfo> storeInfos, SubscriberSystem subscriberSystem, Location location)
        {
            decimal rank = 0;
            _sqlActions = new List<SqlAction>();
            _sqlActions.Add(new SqlAction(rank, GetSqlDecrptyion())); rank++;
            if (storeInfos.Count > 0)
                _sqlActions.AddRange(InsertPSmart(rank, storeInfos, subscriberSystem, location)); rank++;

            StringBuilder sqlBuilder = new StringBuilder(" ");
            foreach (var action in _sqlActions.OrderBy(x => x.Rank))
            {
                sqlBuilder.AppendLine(action.Action);
            }
            return sqlBuilder.ToString();
        }
        private List<SqlAction> InsertPSmart(decimal rank, List<PSmartStoreInfo> storeInfos, SubscriberSystem subscriberSystem, Location location)
        {
           var actions = new List<SqlAction>();
            var maps = subscriberSystem.Maps.Where(x => x.Name == nameof(PSmartStore) && x.HasSubName()).ToList();
            if (maps.Count > 0)
            {
                var mapTbl = maps.FirstOrDefault(x => x.Mode == "Single");


                foreach (var result in storeInfos)
                {
                    Guid mAfyId;
                    mAfyId = result.Uuid;
                    string sqlInitial = $@"

                        UPDATE 
	                        [{mapTbl.SubName}] 
                        SET 
                            [uuid]='{mAfyId}'
                        WHERE 
	                        uuid='{mAfyId}'

                        IF @@ROWCOUNT=0
                            INSERT INTO 
                                    [{mapTbl.SubName}](uuid)
                            VALUES('{mAfyId}');
                    ";

                    actions.Add(new SqlAction(rank, sqlInitial));
                    rank++;


                    foreach (var subscriberMap in maps.Where(x => x.Group == 3 && x.HasSubName()))
                    {
                        string sql = $@"

                        UPDATE 
	                        [{mapTbl.SubName}] 
                        SET 
	                        [{subscriberMap.SubField}]={GetValue(result, subscriberMap, subscriberSystem, 3)}
                        WHERE 
	                        uuid='{mAfyId}';
                    ";
                        actions.Add(new SqlAction(rank, sql));
                        rank++;
                    }

                }
            }

            return actions;
        }

        private static string GetValue(object obj, SubscriberMap subscriberMap, SubscriberSystem subscriberSystem = null, int group = 0)
        {
            var propname = subscriberMap.Field;

            var val = obj.GetPropValue(propname);

            if (null == val)
                return $"NULL";

            if (subscriberMap.Type == "Date")
            {
                DateTime vall = obj.GetPropValue<DateTime>(propname);
                return $"'{vall:yyyy MMM dd}'";
            }
            else
            {
                if (null != subscriberSystem)
                {
                    val = GetTranslation($"{subscriberMap.Name}.{subscriberMap.Field}", val.ToString(),
                        subscriberSystem, group);
                }
            }

            return $"'{val}'";
        }

        public static string GetTranslation(string tref, string tval, SubscriberSystem subscriberSystem, int group = 0)
        {
            var translatio = subscriberSystem.Translations.FirstOrDefault(x => x.Ref.ToLower() == tref.ToLower() && x.Code.ToLower() == tval.ToLower() && x.HasSub() && x.Group == group);
            if (null == translatio)
                return tval;

            return translatio.SubCode;
        }
    }
}
