using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Serilog;

namespace LiveHAPI.IQCare.Infrastructure
{
    public static class EMRContextExtentions
    {
        public static void ApplyMigrations(this EMRContext context)
        {
            try
            {
                context.Database.ExecuteSqlCommand(
                    @"
                IF COL_LENGTH('mst_Patient','mAfyaId') IS NULL
	                BEGIN
		                ALTER TABLE [mst_Patient] ADD [mAfyaId] [uniqueidentifier] NULL
	                END

                IF COL_LENGTH('ord_Visit','mAfyaVisitType') IS NULL
	                BEGIN
		                ALTER TABLE [ord_Visit] ADD [mAfyaVisitType] [int] NULL
	                END
                
                IF OBJECT_ID('dbo.mAfyaView') IS NULL
                    BEGIN
                        EXECUTE('
	                        create view mAfyaView
	                        as
                            SELECT       
	                            Ptn_Pk as Id,
	                            CAST(decryptbykey(FirstName) AS varchar(50)) AS FirstName, 
	                            CAST(decryptbykey(LastName) AS varchar(50)) AS LastName, 
	                            CAST(decryptbykey(MiddleName) AS varchar(50)) AS MiddleName, 
	                            LocationID, 
	                            RegistrationDate, 
	                            DOB, 
	                            Sex, 
	                            DobPrecision, 
	                            HTSID, 
	                            UserID, 
	                            CreateDate, 
	                            UpdateDate, 
	                            DeleteFlag, 
	                            mAfyaId, 
	                            CAST(decryptbykey(Phone) AS varchar(50)) AS Phone,
                                Landmark
                            FROM            
	                            dbo.mst_Patient
                                ')
                    END
            ");

            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                throw;
            }

            try
            {
                context.Database.ExecuteSqlCommand(
                    @"
                IF COL_LENGTH('mst_Patient','mAfyaId') IS NULL
	                BEGIN
		                ALTER TABLE [mst_Patient] ADD [mAfyaId] [uniqueidentifier] NULL
	                END

                IF COL_LENGTH('ord_Visit','mAfyaVisitType') IS NULL
	                BEGIN
		                ALTER TABLE [ord_Visit] ADD [mAfyaVisitType] [int] NULL
	                END
                
                IF OBJECT_ID('dbo.mAfyaView') IS NULL
                    BEGIN
                        EXECUTE('
	                        create view mAfyaView
	                        as
                            SELECT       
	                            Ptn_Pk as Id,
	                            CAST(decryptbykey(FirstName) AS varchar(50)) AS FirstName, 
	                            CAST(decryptbykey(LastName) AS varchar(50)) AS LastName, 
	                            CAST(decryptbykey(MiddleName) AS varchar(50)) AS MiddleName, 
	                            LocationID, 
	                            RegistrationDate, 
	                            DOB, 
	                            Sex, 
	                            DobPrecision, 
	                            HTSID, 
	                            UserID, 
	                            CreateDate, 
	                            UpdateDate, 
	                            DeleteFlag, 
	                            mAfyaId, 
	                            CAST(decryptbykey(Phone) AS varchar(50)) AS Phone,
                                Landmark
                            FROM            
	                            dbo.mst_Patient
                                ')
                    END
            ");
                
            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                throw;
            }
            
            try
            {
                context.Database.ExecuteSqlCommand(
                    @"
                IF OBJECT_ID('dbo.mAfyaFamilyView') IS NULL
                    BEGIN
                        EXECUTE('
	                        create view [dbo].[mAfyaFamilyView]
	                        as
                            SELECT       
	                            Id, 
		                        Ptn_pk,
	                            CAST(decryptbykey(RFirstName) AS varchar(50)) AS FirstName, 
	                            CAST(decryptbykey(RLastName) AS varchar(50)) AS LastName, 
	                            Sex, 
		                        AgeYear, 
		                        AgeMonth, 
		                        RelationshipDate, 
		                        RelationshipType, 
		                        HivStatus, 
		                        HivCareStatus, 
		                        RegistrationNo, 
		                        FileNo, 
		                        ReferenceId, 
		                        UserId, 
		                        DeleteFlag, 
		                        CreateDate, 
		                        UpdateDate
                            FROM            
	                            dbo.dtl_FamilyInfo
                                ')
                    END
            ");

            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                throw;
            }

        }
        public static void UpdateTranslations(this EMRContext context)
        {
           
            try
            {
                string sqlConnectionString = context.Database.GetDbConnection().ConnectionString;

                string script = File.ReadAllText(@"htchapi001.sql");

                SqlConnection conn = new SqlConnection(sqlConnectionString);

                Server server = new Server(new ServerConnection(conn));

                server.ConnectionContext.ExecuteNonQuery(script);

            }
            catch (Exception e)
            {
                Log.Debug($"{e}");
                throw;
            }


        }
    }
}