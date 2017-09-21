using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using LiveHAPI.IQCare.Core.Model;
using LiveHAPI.Shared;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.IQCare.Infrastructure.Repository
{
    public class PatientEncounterRepository : BaseRepository, IPatientEncounterRepository
    {
        private List<SqlAction> _sqlActions;
        public PatientEncounterRepository(EMRContext context) : base(context)
        {
        }

        public void CreateOrUpdate(List<EncounterInfo> encounters ,SubscriberSystem subscriberSystem, Location location)
        {

            var sqlA = GenerateSqlSetupActions( subscriberSystem);
            using (SqlConnection conn = new SqlConnection(Context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlA, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }


            foreach (var encounterInfo in encounters)
            {
                var sql = GenerateSqlVisitActions(encounterInfo, subscriberSystem, location);

                using (SqlConnection conn = new SqlConnection(Context.Database.GetDbConnection().ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                var sql2 = GenerateSqlActions(encounterInfo, subscriberSystem, location);

                using (SqlConnection conn = new SqlConnection(Context.Database.GetDbConnection().ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql2, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        private string GenerateSqlSetupActions(SubscriberSystem subscriberSystem)
        {
            decimal rank = 0;
            _sqlActions = new List<SqlAction>();
            _sqlActions.AddRange(SetUpEncounters(rank, subscriberSystem)); rank++;
            StringBuilder sqlBuilder = new StringBuilder(" ");
            foreach (var action in _sqlActions.OrderBy(x => x.Rank))
            {
                sqlBuilder.AppendLine(action.Action);
            }
            return sqlBuilder.ToString();
        }
        private string GenerateSqlVisitActions(EncounterInfo encounterInfo, SubscriberSystem subscriberSystem, Location location)
        {
            decimal rank = 0;
            _sqlActions = new List<SqlAction>();
            _sqlActions.Add(new SqlAction(rank, GetSqlDecrptyion())); rank++;
            _sqlActions.Add(InsertLabDetailVisit(rank, encounterInfo, subscriberSystem, location)); rank++;
            if (encounterInfo.ObsLinkages.Count > 0)
            {
                _sqlActions.Add(InsertLinkageDetailVisit(rank, encounterInfo, subscriberSystem, location)); rank++;
            }

            StringBuilder sqlBuilder = new StringBuilder(" ");
            foreach (var action in _sqlActions.OrderBy(x => x.Rank))
            {
                sqlBuilder.AppendLine(action.Action);
            }
            return sqlBuilder.ToString();
        }
        private string GenerateSqlActions(EncounterInfo encounterInfo, SubscriberSystem subscriberSystem, Location location)
        {
            decimal rank = 0;
            _sqlActions = new List<SqlAction>();
            _sqlActions.Add(new SqlAction(rank, GetSqlDecrptyion())); rank++;
            //_sqlActions.Add(InsertLabDetail(rank, encounterInfo, subscriberSystem, location)); rank++;
            if (encounterInfo.ObsLinkages.Count > 0) 
                _sqlActions.AddRange(InsertLinkage(rank, encounterInfo, subscriberSystem, location)); rank++;
            

            StringBuilder sqlBuilder = new StringBuilder(" ");
            foreach (var action in _sqlActions.OrderBy(x => x.Rank))
            {
                sqlBuilder.AppendLine(action.Action);
            }
            return sqlBuilder.ToString();
        }
        private List<SqlAction> SetUpEncounters(decimal rank, SubscriberSystem subscriberSystem)
        {
            List<SqlAction> sqlActions=new List<SqlAction>();
            var subscriberMaps = subscriberSystem.Maps.GroupBy(x => x.SubName).Select(x => x.FirstOrDefault());
            foreach (var subscriberMap in subscriberMaps)
            {
                sqlActions.Add(new SqlAction(rank,subscriberMap.GetSqlSetupAction()));
                rank++;
            }
            return sqlActions;
        }
        private SqlAction InsertLabDetailVisit(decimal rank, EncounterInfo encounterInfo, SubscriberSystem subscriberSystem, Location location)
        {
            //Lab.VisitTypeId | 116
            //Linkage.VisitTypeId | 117

              //Registration|VisitTypeId
              var visitType = subscriberSystem.Configs.FirstOrDefault(x => x.Area == "HTS" && x.Name == "Lab.VisitTypeId");

            string sql = $@"

                DECLARE @ptnpk int
                DECLARE @visitipk int
                
                SET @ptnpk=(SELECT TOP 1 Ptn_Pk  FROM mst_Patient WHERE mAfyaId ='{encounterInfo.ClientId}');       

                UPDATE 
	                [ord_Visit] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [LocationID]='{location.FacilityID}',
                    [VisitDate]='{encounterInfo.EncounterDate:yyyy MMM dd}',
                    [VisitType]= {visitType.Value},
                    [DataQuality]='0',
                    [UserID]='0',
                    [Signature]='0',
                    [UpdateDate]=GETDATE(),
                    [mAfyaVisitType]=1
                WHERE 
	                Ptn_pk=@ptnpk AND LocationId={location.FacilityID} AND mAfyaVisitType=1 AND [VisitType]={visitType.Value} 
                IF @@ROWCOUNT=0
                    INSERT INTO 
                        ord_Visit(Ptn_Pk, LocationID, VisitDate, VisitType,DataQuality,UserID,Signature,CreateDate,mAfyaVisitType)
                    VALUES(
                        @ptnpk,'{location.FacilityID}', '{encounterInfo.EncounterDate:yyyy MMMM dd}', {visitType.Value},'0', '0','0', GETDATE(),1);
                
                SET @visitipk=(SELECT TOP 1 [Visit_Id] FROM [ord_Visit] WHERE Ptn_Pk=@ptnpk AND mAfyaVisitType=1 AND [VisitType]={visitType.Value} ORDER BY CreateDate desc);
";

            var action = new SqlAction(rank, sql);
            return action;
        }
        private SqlAction InsertLabDetail(decimal rank, EncounterInfo encounterInfo, SubscriberSystem subscriberSystem, Location location)
        {
            
            //Lab.VisitTypeId | 116
            //Linkage.VisitTypeId | 117

            //Registration|VisitTypeId
            var visitType = subscriberSystem.Configs.FirstOrDefault(x => x.Area == "HTS" && x.Name == "Lab.VisitTypeId");

            string sql = $@"

                DECLARE @ptnpk int
                DECLARE @visitipk int
                
                SET @ptnpk=(SELECT TOP 1 Ptn_Pk  FROM mst_Patient WHERE mAfyaId ='{encounterInfo.ClientId}');       

                UPDATE 
	                [ord_Visit] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [LocationID]='{location.FacilityID}',
                    [VisitDate]='{encounterInfo.EncounterDate:yyyy MMM dd}',
                    [VisitType]= {visitType.Value},
                    [DataQuality]='0',
                    [UserID]='0',
                    [Signature]='0',
                    [UpdateDate]=GETDATE(),
                    [mAfyaVisitType]=1
                WHERE 
	                Ptn_pk=@ptnpk AND LocationId={location.FacilityID} AND mAfyaVisitType=1 AND [VisitType]={visitType.Value} 
                IF @@ROWCOUNT=0
                    INSERT INTO 
                        ord_Visit(Ptn_Pk, LocationID, VisitDate, VisitType,DataQuality,UserID,Signature,CreateDate,mAfyaVisitType)
                    VALUES(
                        @ptnpk,'{location.FacilityID}', '{encounterInfo.EncounterDate:yyyy MMMM dd}', {visitType.Value},'0', '0','0', GETDATE(),1);
                
                SET @visitipk=(SELECT TOP 1 [Visit_Id] FROM [ord_Visit] WHERE Ptn_Pk=@ptnpk AND mAfyaVisitType=1 AND [VisitType]={visitType.Value} ORDER BY CreateDate desc);
";

            var action = new SqlAction(rank, sql);
            return action;
        }
        private SqlAction InsertLinkageDetailVisit(decimal rank, EncounterInfo encounterInfo, SubscriberSystem subscriberSystem, Location location)
        {
            //Lab.VisitTypeId | 116
            //Linkage.VisitTypeId | 117

            //Registration|VisitTypeId
            var visitType = subscriberSystem.Configs.FirstOrDefault(x => x.Area == "HTS" && x.Name == "Linkage.VisitTypeId");

            string sql = $@"

                UPDATE 
	                [ord_Visit] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [LocationID]='{location.FacilityID}',
                    [VisitDate]='{encounterInfo.EncounterDate:yyyy MMM dd}',
                    [VisitType]= {visitType.Value},
                    [DataQuality]='0',
                    [UserID]='0',
                    [Signature]='0',
                    [UpdateDate]=GETDATE(),
                    [mAfyaVisitType]=1
                WHERE 
	                Ptn_pk=@ptnpk AND LocationId={location.FacilityID} AND mAfyaVisitType=1 AND [VisitType]={visitType.Value} 
                IF @@ROWCOUNT=0
                    INSERT INTO 
                        ord_Visit(Ptn_Pk, LocationID, VisitDate, VisitType,DataQuality,UserID,Signature,CreateDate,mAfyaVisitType)
                    VALUES(
                        @ptnpk,'{location.FacilityID}', '{encounterInfo.EncounterDate:yyyy MMMM dd}', {visitType.Value},'0', '0','0', GETDATE(),1);
                
                SET @visitipk=(SELECT TOP 1 [Visit_Id] FROM [ord_Visit] WHERE Ptn_Pk=@ptnpk AND mAfyaVisitType=1 AND [VisitType]={visitType.Value} ORDER BY CreateDate desc);";

            var action = new SqlAction(rank, sql);
            return action;
        }
        private List<SqlAction> InsertLinkage(decimal rank, EncounterInfo encounter, SubscriberSystem subscriberSystem, Location location)
        {
            //Lab.VisitTypeId | 116
            //Linkage.VisitTypeId | 117

            //Registration|VisitTypeId
            var visitType = subscriberSystem.Configs.FirstOrDefault(x => x.Area == "HTS" && x.Name == "Linkage.VisitTypeId");

            //GET MAP
            var actions=new List<SqlAction>();

            var maps = subscriberSystem.Maps.Where(x => x.Name == nameof(ObsLinkage)).ToList();

            if (maps.Count > 0)
            {
                //SINGLE

                var mAfyId = encounter.ObsLinkages.FirstOrDefault().Id;
                var mapTbl = maps.Where(x => x.Mode == "Single").Select(x => x.SubName).Distinct().FirstOrDefault();



                string sql22 = $@"

                DECLARE @ptnpk int
                DECLARE @visitipk int
                
                SET @ptnpk=(SELECT TOP 1 Ptn_Pk  FROM mst_Patient WHERE mAfyaId ='{encounter.ClientId}');               
                SET @visitipk=(SELECT TOP 1 Ptn_Pk  FROM  ord_visit WHERE (Ptn_Pk = @ptnpk) AND (VisitType = {visitType.Value}) AND (mAfyaVisitType = 1));       

                        UPDATE 
	                        [{mapTbl}] 
                        SET 
	                        [mAfyaId]='{mAfyId}',
                            [Visit_Pk]=@visitipk,                    
                            [LocationID]='{location.FacilityID}',
                            [UserID]='0',                
                            [UpdateDate]=GETDATE()
                        WHERE 
	                        mAfyaId='{mAfyId}'

                        IF @@ROWCOUNT=0
                            INSERT INTO 
                                    [{mapTbl}](
                                    ptn_pk, Visit_Pk, LocationID, UserID, CreateDate,mAfyaId)
                            VALUES(@ptnpk,@visitipk, 
                                {location.FacilityID}, 0, GETDATE(),'{mAfyId}');
                    ";

                actions.Add(new SqlAction(rank,sql22));
                rank++;

                var obsLinkage = encounter.ObsLinkages.FirstOrDefault();

                

                if(null!=obsLinkage)
                {
                    foreach (var subscriberMap in maps)
                    {
                        string sql223 = $@"

                        UPDATE 
	                        [{mapTbl}] 
                        SET 
	                        [{subscriberMap.SubField}]= {GetValue(obsLinkage,subscriberMap)}
                        WHERE 
	                        mAfyaId='{mAfyId}';
                    ";
                        actions.Add(new SqlAction(rank, sql223));
                        rank++;
                    }
                }
            }

            return actions;
        }
        
        private static string GetValue(object obj, SubscriberMap subscriberMap)
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
            
            return $"'{val}'";
        }
    }
}
