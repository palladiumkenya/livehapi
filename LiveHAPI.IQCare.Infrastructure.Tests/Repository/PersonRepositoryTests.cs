﻿using System;
using System.Data.Common;
using System.Linq;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.IQCare.Core.Interfaces.Repository;
using LiveHAPI.IQCare.Core.Model;
using LiveHAPI.IQCare.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Dapper;

namespace LiveHAPI.IQCare.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class PersonRepositoryTests
    {
        private EMRContext  _context;
        private IPatientRepository _patientRepository;
        private IConfigRepository _configRepository;
        private ISubscriberSystemRepository _subscriberSystemRepository;
        private Patient patient;
        private SubscriberSystem subscriberSystem;
        private Location location;
        private ClientInfo _client;
        private DbConnection _db;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:EMRConnection"];
            var options = new DbContextOptionsBuilder<EMRContext>()
                .UseSqlServer(connectionString)
                .Options;

            var connectionString2 = config["connectionStrings:hAPIConnection"];
            var options2 = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString2)
                .Options;


            _context = new EMRContext(options);
            _context.ApplyMigrations();
            _subscriberSystemRepository = new SubscriberSystemRepository(new LiveHAPIContext(options2));
            subscriberSystem = _subscriberSystemRepository.GetDefault();
          _configRepository = new ConfigRepository(_context);
            location = _configRepository.GetLocations().FirstOrDefault();
            _patientRepository=new PatientRepository(_context);
            _client = TestData.TestClientInfo();
             patient = Patient.Create(_client, location.FacilityID);
            _db = _context.Database.GetDbConnection();
        }

        
        [Test]
        public void should_CreateOrUpdate()
        {
            _patientRepository.CreateOrUpdate(patient,subscriberSystem,location);
            var savePatient = _patientRepository.Get(patient.mAfyaId.Value);
            Assert.IsNotNull(savePatient);
            Console.WriteLine($"Patient: {savePatient}");
            Assert.AreEqual("201707001", savePatient.HTSID);
            Assert.True(savePatient.Id > -1);

            var regVisitTypeId = subscriberSystem.Configs.First(x => x.Area == "Registration" && x.Name == "VisitTypeId").Value;
            var htsVisitTypeId = subscriberSystem.Configs.First(x => x.Area == "HTS" && x.Name == "Enrollment.VisitTypeId").Value;
            var moduleId = subscriberSystem.Configs.First(x => x.Area == "HTS" && x.Name == "ModuleId").Value;

            Assert.AreEqual(1,_db.ExecuteScalar($"select count(Ptn_Pk)  from  [mst_Patient] where Ptn_Pk in ({savePatient.Id})"));
            Assert.True(_db.ExecuteScalar($"SELECT IQNumber FROM mst_Patient WHERE Ptn_Pk = {savePatient.Id}").ToString().Contains("IQ-"));
            Assert.AreEqual(1,_db.ExecuteScalar($"select count(Ptn_Pk)  from  [ord_Visit] where Ptn_Pk in ({savePatient.Id}) AND VisitType={regVisitTypeId}"));
            Assert.AreEqual(1, _db.ExecuteScalar($"select count(Ptn_Pk)  from  [ord_Visit] where Ptn_Pk in ({savePatient.Id}) AND VisitType={htsVisitTypeId}"));
            Assert.AreEqual(1, _db.ExecuteScalar($"select count(Ptn_Pk)  from  [lnk_patientprogramstart] where Ptn_Pk in ({savePatient.Id}) AND ModuleID={moduleId}"));
        }

        [Test]
        public void TearDown()
        {
            //  4700b0e0-00c0-0c0f-0d0a-a0b0000df000

            _context.Database.ExecuteSqlCommand(@"
            delete from  dtl_PatientContacts where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  [DTL_PATIENTHOUSEHOLDINFO] where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
            delete from  [DTL_RURALRESIDENCE] where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
            delete from  [DTL_URBANRESIDENCE]  where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
            delete from  [DTL_PATIENTHIVPREVCAREENROLLMENT]  where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');			
            delete from  ord_Visit where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  mst_Patient where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  lnk_patientprogramstart where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            ");
        }
    }
} 