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
using Serilog;


namespace LiveHAPI.IQCare.Infrastructure.Repository
{
    public class PatientFamilyRepository : BaseRepository, IPatientFamilyRepository
    {
        
        public PatientFamilyRepository(EMRContext context) : base(context)
        {
        }
        public IEnumerable<PatientFamily> GetMembers(int patientPk)
        {
            var db = Context.Database.GetDbConnection();
            var patient =db.Query<PatientFamily>($"{GetSqlDecrptyion()} SELECT * FROM mAfyaFamilyView WHERE Ptn_Pk='{patientPk}'");
            return patient;
        }
    }
}
