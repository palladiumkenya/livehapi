using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EFCore.BulkExtensions;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using LiveHAPI.IQCare.Core.Model;
using LiveHAPI.Shared;
using LiveHAPI.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace LiveHAPI.IQCare.Infrastructure.Repository
{
    public class PatientRepository : BaseRepository, IPatientRepository
    {
        private List<SqlAction> _sqlActions;
        public PatientRepository(EMRContext context) : base(context)
        {
            
        }

        public Patient Get(Guid id)
        {
            var db = Context.Database.GetDbConnection();
            var patient = db.Query<Patient>($"{GetSqlDecrptyion()} SELECT * FROM mAfyaView WHERE mAfyaId='{id}'").FirstOrDefault();
            return patient;
        }

        public void CreateOrUpdate(Patient patient, SubscriberSystem subscriberSystem, Location location)
        {
            var sql = GenerateSqlActions(patient, subscriberSystem, location);

            using (SqlConnection conn = new SqlConnection(Context.Database.GetDbConnection().ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }


           
        }

        private string GenerateSqlActions(Patient patient, SubscriberSystem subscriberSystem, Location location)
        {
            decimal rank = 0;
            _sqlActions = new List<SqlAction>();
            _sqlActions.Add(new SqlAction(rank, GetSqlDecrptyion())); rank++;
            _sqlActions.Add(InsertPatient(rank, patient, subscriberSystem, location)); rank++;
            _sqlActions.Add(InsertVisit(rank, patient, subscriberSystem, location)); rank++;
            _sqlActions.Add(InsertContacts(rank, patient, subscriberSystem, location)); rank++;
            _sqlActions.Add(InsertDefualts(rank, patient, subscriberSystem, location)); rank++;
            _sqlActions.Add(InsertRegistration(rank, patient, subscriberSystem, location)); rank++;
            _sqlActions.Add(UpdateReference(rank)); rank++;
            _sqlActions.Add(InsertEnrollment(rank, patient, subscriberSystem, location)); rank++;

            StringBuilder sqlBuilder = new StringBuilder(" ");
            foreach (var action in _sqlActions.OrderBy(x => x.Rank))
            {
                sqlBuilder.AppendLine(action.Action);
            }
            return sqlBuilder.ToString();
        }

        private SqlAction InsertPatient(decimal rank, Patient patient, SubscriberSystem subscriberSystem, Location location)
        {
            //TODO: ALTER TABLE [dbo].[mst_Patient] ADD [mAfyaId] [uniqueidentifier] NULL

            string sql = $@"
                DECLARE @ptnpk int
                DECLARE @visitipk int

                UPDATE 
	                [mst_Patient] 
                SET 
	                [Status]='0',
                    [FirstName]=encryptbykey(key_guid('Key_CTC'), '{patient.FirstName}'),
                    [MiddleName]=encryptbykey(key_guid('Key_CTC'), '{patient.MiddleName}'),
                    [LastName]=encryptbykey(key_guid('Key_CTC'), '{patient.LastName}'),    

                    [LocationID]=  '{location.FacilityID}',
                    [RegistrationDate]= '{patient.RegistrationDate:yyyy MMMM dd}',
                    [Sex]= '{patient.Sex}', 
                    [DOB]= '{patient.Dob:yyyy MMMM dd}',
                    [DobPrecision]= '{patient.DobPrecision}', 

                    [CountryId]='{location.CountryID}',
                    [PosId]='{location.PosID}',
                    [SatelliteId]='{location.SatelliteID}', 
                    [UserID]='1', 
                    [UpdateDate]=GETDATE(),    

                    [Phone]= encryptbykey(key_guid('Key_CTC'), '{patient.Phone}'),
                    [HTSID]= '{patient.HTSID}'

                WHERE 
	                mAfyaId='{patient.mAfyaId}'

                IF @@ROWCOUNT=0

                    INSERT INTO 
                        mst_Patient(
                            Status, FirstName, MiddleName, LastName, 
                            LocationID, RegistrationDate, Sex, DOB, DobPrecision,
                            CountryId, PosId, SatelliteId, UserID, CreateDate,
                            Phone,HTSID,mAfyaId)
                    VALUES(
                        '0', encryptbykey(key_guid('Key_CTC'), '{patient.FirstName}'), encryptbykey(key_guid('Key_CTC'), '{patient.MiddleName}'), encryptbykey(key_guid('Key_CTC'), '{patient.LastName}'), 
                        '{location.FacilityID}', '{patient.RegistrationDate:yyyy MMMM dd}', '{patient.Sex}', '{patient.Dob:yyyy MMMM dd}', '{patient.DobPrecision}', 
                        '{location.CountryID}', '{location.PosID}', '{location.SatelliteID}', '{patient.UserId}', GETDATE(),
                        encryptbykey(key_guid('Key_CTC'), '{patient.Phone}'),'{patient.HTSID}','{patient.mAfyaId}');
                
                SET @ptnpk=(SELECT Ptn_Pk  FROM mst_Patient WHERE mAfyaId ='{patient.mAfyaId}');";


            var action = new SqlAction(rank, sql);
            return action;
        }
        
        private SqlAction InsertVisit(decimal rank, Patient patient, SubscriberSystem subscriberSystem, Location location)
        {

            //Registration|VisitTypeId
            var visitType = subscriberSystem.Configs.FirstOrDefault(x => x.Area == "Registration" && x.Name == "VisitTypeId");

            string sql = $@"

                UPDATE 
	                [ord_Visit] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [LocationID]='{location.FacilityID}',
                    [VisitDate]='{patient.RegistrationDate:yyyy MMMM dd}',
                    [VisitType]= {visitType.Value},
                    [UserID]='{patient.UserId}',
                    [UpdateDate]=GETDATE(),
                    [mAfyaVisitType]=1
                WHERE 
	                Ptn_pk=@ptnpk AND LocationId={location.FacilityID} AND mAfyaVisitType=1 AND VisitType={visitType.Value}         
                IF @@ROWCOUNT=0
                    INSERT INTO 
                        ord_Visit(
                            Ptn_Pk, LocationID, VisitDate, VisitType, UserID, CreateDate,mAfyaVisitType)
                    VALUES(
                        @ptnpk,'{location.FacilityID}', '{patient.RegistrationDate:yyyy MMMM dd}', {visitType.Value}, '{patient.UserId}', GETDATE(),1);
                
                SET @visitipk=(SELECT TOP 1 [Visit_Id] FROM [ord_Visit] WHERE Ptn_Pk=@ptnpk AND mAfyaVisitType=1 ORDER BY CreateDate desc);";

            var action = new SqlAction(rank, sql);
            return action;
        }
        
        private SqlAction InsertContacts(decimal rank, Patient patient, SubscriberSystem subscriberSystem, Location location)
        {
            string sql = $@"
                
                UPDATE 
	                [dtl_PatientContacts] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [VisitId]=@visitipk,                    
                    [LocationID]='{location.FacilityID}',
                    [UserID]='{patient.UserId}',                
                    [UpdateDate]=GETDATE()
                WHERE 
	                Ptn_pk=@ptnpk AND VisitId=@visitipk
                IF @@ROWCOUNT=0
                    INSERT INTO 
                        dtl_PatientContacts(
                            ptn_pk, VisitId, LocationID, UserID, CreateDate)
                    VALUES(@ptnpk,@visitipk, 
                        {location.FacilityID}, {patient.UserId}, GETDATE());";

            var action = new SqlAction(rank, sql);
            return action;
        }
        
        private SqlAction InsertDefualts(decimal rank, Patient patient, SubscriberSystem subscriberSystem, Location location)
        {
            string sql = $@"

                UPDATE 
	                [DTL_PATIENTHOUSEHOLDINFO] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [Visit_Pk]=@visitipk,                    
                    [LocationID]='{location.FacilityID}',
                    [UserID]='{patient.UserId}',
                    [UpdateDate]=GETDATE()
                WHERE 
	                Ptn_pk=@ptnpk AND Visit_Pk=@visitipk
                IF @@ROWCOUNT=0

                    Insert into 
	                    [DTL_PATIENTHOUSEHOLDINFO](
		                    Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate)
	                    Values(
		                    @ptnpk,@visitipk, {location.FacilityID}, {patient.UserId}, GetDate());


                UPDATE 
	                [DTL_RURALRESIDENCE] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [Visit_Pk]=@visitipk,                    
                    [LocationID]='{location.FacilityID}',
                    [UserID]='{patient.UserId}',
                    [RuralDistrict]='0',
                    [RuralDivision]='0',
                    [UpdateDate]=GETDATE()
                WHERE 
	                Ptn_pk=@ptnpk AND Visit_Pk=@visitipk
                IF @@ROWCOUNT=0                    
                    Insert into 
	                    [DTL_RURALRESIDENCE](
		                    Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,[RuralDistrict],[RuralDivision])
	                    Values(
		                    @ptnpk,@visitipk,{location.FacilityID}, {patient.UserId}, GetDate(),'0','0');
                
               
                UPDATE 
	                [DTL_URBANRESIDENCE] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [Visit_Pk]=@visitipk,                    
                    [LocationID]='{location.FacilityID}',
                    [UserID]='{patient.UserId}',
                    [UrbanTown]='0',                    
                    [UpdateDate]=GETDATE()
                WHERE 
	                Ptn_pk=@ptnpk AND Visit_Pk=@visitipk
                IF @@ROWCOUNT=0
                    Insert into 
	                    [DTL_URBANRESIDENCE](
		                    Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,[UrbanTown])
	                    Values(
		                    @ptnpk,@visitipk,{location.FacilityID}, {patient.UserId}, GetDate(),'0');
                

                UPDATE 
	                [DTL_PATIENTHIVPREVCAREENROLLMENT] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [Visit_Pk]=@visitipk,                    
                    [LocationID]='{location.FacilityID}',
                    [UserID]='{patient.UserId}',
                    [UpdateDate]=GETDATE()
                WHERE 
	                Ptn_pk=@ptnpk AND Visit_Pk=@visitipk
                IF @@ROWCOUNT=0
                    Insert into 
	                    [DTL_PATIENTHIVPREVCAREENROLLMENT](
		                    Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate)
	                    Values(
		                    @ptnpk,@visitipk, {location.FacilityID}, {patient.UserId}, GetDate());
		

                UPDATE 
	                [DTL_PATIENTGUARANTOR] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [Visit_Pk]=@visitipk,                    
                    [LocationID]='{location.FacilityID}',
                    [UserID]='{patient.UserId}',
                    [Guarantor1Occupation]='0',
                    [Guarantor2Occupation]='0',
                    [UpdateDate]=GETDATE()
                WHERE 
	                Ptn_pk=@ptnpk AND Visit_Pk=@visitipk
                IF @@ROWCOUNT=0
                    Insert into 
	                    [DTL_PATIENTGUARANTOR](
		                    Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,[Guarantor1Occupation],[Guarantor2Occupation])
	                    Values(
		                    @ptnpk,@visitipk, {location.FacilityID}, {patient.UserId}, GetDate(),'0','0');


                UPDATE 
	                [DTL_PATIENTDEPOSITS] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [Visit_Pk]=@visitipk,                    
                    [LocationID]='{location.FacilityID}',
                    [UserID]='{patient.UserId}',
                    [UpdateDate]=GETDATE()
                WHERE 
	                Ptn_pk=@ptnpk AND Visit_Pk=@visitipk
                IF @@ROWCOUNT=0
                    Insert into 
	                    [DTL_PATIENTDEPOSITS](
		                    Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate)
	                    Values(
		                    @ptnpk,@visitipk, {location.FacilityID}, {patient.UserId}, GetDate());
		

                UPDATE 
	                [DTL_PATIENTINTERVIEWER] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [Visit_Pk]=@visitipk,                    
                    [LocationID]='{location.FacilityID}',
                    [UserID]='{patient.UserId}',
                    [UpdateDate]=GETDATE()
                WHERE 
	                Ptn_pk=@ptnpk AND Visit_Pk=@visitipk
                IF @@ROWCOUNT=0
                    Insert into 
	                    [DTL_PATIENTINTERVIEWER](
		                    Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate)
	                    Values(
		                    @ptnpk,@visitipk, {location.FacilityID}, {patient.UserId}, GetDate());";

            var action = new SqlAction(rank, sql);
            return action;
        }
        
        private SqlAction InsertRegistration(decimal rank, Patient patient, SubscriberSystem subscriberSystem, Location location)
        {
            string sql = $@"
                if exists(
	                select name from sysobjects where name = 'DTL_FBCUSTOMFIELD_Patient_Registration') 
                begin                     
                    UPDATE 
	                    [DTL_FBCUSTOMFIELD_Patient_Registration] 
                    SET 
	                    [Ptn_Pk]=@ptnpk,
                        [Visit_Pk]=@visitipk,                    
                        [LocationID]='{location.FacilityID}',
                        [UserID]='{patient.UserId}',
                        [UpdateDate]=GETDATE()
                    WHERE 
	                    Ptn_pk=@ptnpk AND Visit_Pk=@visitipk
                    IF @@ROWCOUNT=0
	                    Insert into 
		                    [DTL_FBCUSTOMFIELD_Patient_Registration](
			                    Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate)
		                    Values(
			                    @ptnpk,@visitipk,{location.FacilityID},{patient.UserId}, GetDate()) 
                end ;";
            var action = new SqlAction(rank, sql);
            return action;
        }
        
        private SqlAction UpdateReference(decimal rank)
        {
            string sql = $@"
                update 
	                mst_patient set IQNumber = 'IQ-'+convert(varchar,Replicate('0',20-len(x.[ptnIdentifier]))) +x.[ptnIdentifier]  
                from (
	                select 
		                UPPER(substring(convert(varchar(50),decryptbykey(firstname)),1,1))+UPPER(substring(convert(varchar(50),decryptbykey(lastname)),1,1))+convert(varchar,dob,112)+convert(varchar,locationid)+Convert(varchar(10),ptn_pk) [ptnIdentifier] 
	                from 
		                mst_patient where ptn_pk = @ptnpk)x 
	                where ptn_pk= @ptnpk;";

            var action = new SqlAction(rank, sql);
            return action;
        }
        
        private SqlAction InsertEnrollment(decimal rank, Patient patient, SubscriberSystem subscriberSystem, Location location)
        {
            //HTS|VisitTypeId

            var visitType = subscriberSystem.Configs.FirstOrDefault(x => x.Area == "HTS" && x.Name == "Enrollment.VisitTypeId");

            var module = subscriberSystem.Configs.FirstOrDefault(x => x.Area == "HTS" && x.Name == "ModuleId");

            string sql = $@"

                UPDATE 
	                [ord_Visit] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [LocationID]='{location.FacilityID}',
                    [VisitDate]='{patient.RegistrationDate:yyyy MMMM dd}',
                    [VisitType]= {visitType.Value},
                    [DataQuality]=0,
                    [UserID]='{patient.UserId}',
                    [UpdateDate]=GETDATE(),
                    [mAfyaVisitType]=1
                WHERE 
	                Ptn_pk=@ptnpk AND LocationId={location.FacilityID} AND mAfyaVisitType=1 AND VisitType={visitType.Value}            
                IF @@ROWCOUNT=0
                    Insert into 
	                    ord_visit(
		                    Ptn_Pk,LocationID,VisitDate,VisitType,DataQuality,DeleteFlag,UserID,CreateDate,mAfyaVisitType)
	                    values (
		                    @ptnpk,{location.FacilityID},'{patient.RegistrationDate:yyyy MMMM dd}',{visitType.Value},0,0,{patient.UserId}, Getdate(),1);

                UPDATE 
	                [lnk_patientprogramstart] 
                SET 
	                [Ptn_Pk]=@ptnpk,
                    [ModuleID]={module.Value},
                    [StartDate]='{patient.RegistrationDate:yyyy MMMM dd}',
                    [UserID]='{patient.UserId}',
                    [UpdateDate]=GETDATE()
                WHERE 
	                Ptn_pk=@ptnpk AND ModuleID={module.Value}            
                IF @@ROWCOUNT=0                  
                    Insert into 
	                    lnk_patientprogramstart(
		                    Ptn_Pk,ModuleID,StartDate,UserID,CreateDate)
	                    values (
		                    @ptnpk, {module.Value},'{patient.RegistrationDate:yyyy MMMM dd}',{patient.UserId}, Getdate());";


            var action = new SqlAction(rank, sql);
            return action;
        }
    }
}
