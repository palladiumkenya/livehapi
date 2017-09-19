using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.IQCare.Infrastructure
{
    public static class EMRContextExtentions
    {
        public static void ApplyMigrations(this EMRContext context)
        {
            context.Database.ExecuteSqlCommand(
             @"
                IF COL_LENGTH('mst_Patient','mAfyaId') IS NULL
	                BEGIN
		                ALTER TABLE [mst_Patient] ADD [mAfyaId] [uniqueidentifier] NULL
	                END


                IF OBJECT_ID('dbo.mAfyaView') IS NULL
                    BEGIN
                         EXECUTE('
	                    create view mAfyaView
	                    as
	                    SELECT        
		                    Ptn_Pk, 
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
		                    DeleteFlag
	                    FROM            
		                    mst_Patient')
                    END
            ");
        }
    }
}