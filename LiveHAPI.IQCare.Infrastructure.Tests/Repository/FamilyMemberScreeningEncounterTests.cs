using System;
using System.Collections.Generic;
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
    public class FamilyMemberScreeningEncounterTests
    {
        private EMRContext  _context;
        private IPatientEncounterRepository _patientEncounterRepository;
        private IConfigRepository _configRepository;
        private ISubscriberSystemRepository _subscriberSystemRepository;
        private SubscriberSystem _subscriberSystem;
        private Location _location;
        private Patient _patient, _patientSon;
        private List<EncounterInfo> _encounterInfo;
        private DbConnection _db;
        private ClientInfo _client, _clientSon;
        private IPatientRepository _patientRepository;
        private DbContextOptions<EMRContext> _options;
        private DbContextOptions<LiveHAPIContext> _options2;


        [OneTimeSetUp]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:EMRConnection"];
            _options = new DbContextOptionsBuilder<EMRContext>()
                .UseSqlServer(connectionString)
                .Options;

            var connectionString2 = config["connectionStrings:hAPIConnection"];
            _options2 = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString2)
                .Options;

            var context = new EMRContext(_options);
            context.ApplyMigrations();
            context.UpdateTranslations();

        }

        [SetUp]
        public void SetUp()
        {
            _context = new EMRContext(_options);
            _subscriberSystemRepository = new SubscriberSystemRepository(new LiveHAPIContext(_options2));
            _subscriberSystem = _subscriberSystemRepository.GetDefault();
          _configRepository = new ConfigRepository(_context);
            _location = _configRepository.GetLocations().FirstOrDefault();
            _patientEncounterRepository=new PatientEncounterRepository(_context);
            _patientRepository = new PatientRepository(_context);
            _client = TestData.TestClientInfo3();
            _clientSon = TestData.TestClientInfo4();
            _patient = Patient.Create(_client, _location.FacilityID, _subscriberSystem);
            _patientSon = Patient.Create(_clientSon, _location.FacilityID, _subscriberSystem);
            _encounterInfo = TestData.TestFamilyEncounterInfoData();             
            _db = _context.Database.GetDbConnection();

            _patientRepository.CreateOrUpdate(_patient, _subscriberSystem, _location);
            _patientRepository.CreateOrUpdateRelations(_client.Id, _client.Relationships, _subscriberSystem, _location);
            _patientRepository.CreateOrUpdate(_patientSon, _subscriberSystem, _location);
            _patientRepository.CreateOrUpdateRelations(_clientSon.Id, _clientSon.Relationships, _subscriberSystem, _location);
        }
        
        [Test]
        public void should_CreateOrUpdate_FamilyMemberScreening_New_()
        {
            var savePatient = _patientRepository.Get(_patientSon.mAfyaId.Value);
            Assert.IsNotNull(savePatient);

            _patientEncounterRepository.CreateOrUpdate(_encounterInfo,_subscriberSystem,_location);

            var familyScreeningVisitTypeId = _subscriberSystem.Configs.First(x => x.Area == "HTS" && x.Name == "Family.VisitTypeId").Value;
            Assert.AreEqual(1, _db.ExecuteScalar($"select count(Ptn_Pk)  from  [ord_Visit] where Ptn_Pk in ({savePatient.Id}) AND VisitType={familyScreeningVisitTypeId}"));
        
            var screeings =_db.ExecuteScalar($"select count(Ptn_Pk)  from  [DTL_FBCUSTOMFIELD_FamilyMemberTesting] where Ptn_Pk in ({savePatient.Id})");
            Assert.True(Convert.ToInt32(screeings) > 0);
        }
       
        [TearDown]
        public void TearDown()
        {
            //  4700b0e0-00c0-0c0f-0d0a-a0b0000df000

            _context.Database.ExecuteSqlCommand(@"
            delete from  dtl_PatientContacts where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  [DTL_PATIENTHOUSEHOLDINFO] where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
            delete from  [DTL_RURALRESIDENCE] where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
            delete from  [DTL_URBANRESIDENCE]  where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
            delete from  [DTL_PATIENTHIVPREVCAREENROLLMENT]  where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
            delete from  [DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362]  where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
            delete from  [DTL_FBCUSTOMFIELD_LinkageAndTracking]  where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
            delete from  [DTL_CUSTOMFORM_HTS Tracing_LinkageAndTracking]  where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
	        delete from  [DTL_CUSTOMFORM_HIV-Test 1_HTC_Lab_MOH_362]  where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
			delete from  [DTL_CUSTOMFORM_HIV-Test 2_HTC_Lab_MOH_362]  where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
	        delete from  [DTL_FBCUSTOMFIELD_FamilyMemberTesting]  where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  ord_Visit where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  mst_Patient where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  lnk_patientprogramstart where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  dtl_FamilyInfo where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            ");
        }
    }
}
