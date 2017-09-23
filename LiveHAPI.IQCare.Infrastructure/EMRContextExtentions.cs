using System;
using Microsoft.EntityFrameworkCore;
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
            }
        }
    }
}