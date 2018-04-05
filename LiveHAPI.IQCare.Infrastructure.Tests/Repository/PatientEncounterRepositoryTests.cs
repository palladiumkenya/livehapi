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
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.IQCare.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class PatientEncounterRepositoryTests
    {
        private EMRContext  _context;
        private IPatientEncounterRepository _patientEncounterRepository;
        private IConfigRepository _configRepository;
        private ISubscriberSystemRepository _subscriberSystemRepository;
        private SubscriberSystem subscriberSystem;
        private Location location;
        private Patient _patient, _patient2;
        private List<EncounterInfo> _encounterInfo, _encounterInfo2;
        private DbConnection _db;
        private ClientInfo _client,_client2;
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
            subscriberSystem = _subscriberSystemRepository.GetDefault();
          _configRepository = new ConfigRepository(_context);
            location = _configRepository.GetLocations().FirstOrDefault();
            _patientEncounterRepository=new PatientEncounterRepository(_context);
            _patientRepository = new PatientRepository(_context);
            _client = TestData.TestClientInfo();
            _client2 = TestData.TestClientInfo2();
            _patient = Patient.Create(_client, location.FacilityID, subscriberSystem);
            _patient2= Patient.Create(_client2, location.FacilityID, subscriberSystem);
            _encounterInfo = TestData.TestEncounterInfoData();
            _encounterInfo2 = TestData.TestEncounterInfoData2();
            foreach (var encounterInfo in _encounterInfo2)
            {
                encounterInfo.ClientId = _client2.Id;
                encounterInfo.Id = LiveGuid.NewGuid();
                foreach (var obs in encounterInfo.Obses)
                {
                    obs.Id = LiveGuid.NewGuid();
                    obs.EncounterId = encounterInfo.Id;
                }
            }

            _db = _context.Database.GetDbConnection();

            _patientRepository.CreateOrUpdate(_patient, subscriberSystem, location);
            _patientRepository.CreateOrUpdate(_patient2, subscriberSystem, location);
        }
        
        [Test]
        public void should_CreateOrUpdate_New()
        {
            var savePatient = _patientRepository.Get(_patient.mAfyaId.Value);
            Assert.IsNotNull(savePatient);

            _patientEncounterRepository.CreateOrUpdate(_encounterInfo,subscriberSystem,location);

            var labVisitTypeId = subscriberSystem.Configs.First(x => x.Area == "HTS" && x.Name == "Lab.VisitTypeId").Value;
            var linkVisitTypeId = subscriberSystem.Configs.First(x => x.Area == "HTS" && x.Name == "Linkage.VisitTypeId").Value;

            Assert.AreEqual(1, _db.ExecuteScalar($"select count(Ptn_Pk)  from  [ord_Visit] where Ptn_Pk in ({savePatient.Id}) AND VisitType={labVisitTypeId}"));
            Assert.AreEqual(1, _db.ExecuteScalar($"select count(Ptn_Pk)  from  [ord_Visit] where Ptn_Pk in ({savePatient.Id}) AND VisitType={linkVisitTypeId}"));
            Assert.AreEqual(1, _db.ExecuteScalar($"select count(Ptn_Pk)  from  [DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] where Ptn_Pk in ({savePatient.Id})"));

            var traces =_db.ExecuteScalar($"select count(Ptn_Pk)  from  [DTL_CUSTOMFORM_HTS Tracing_LinkageAndTracking] where Ptn_Pk in ({savePatient.Id})");
            Assert.True(Convert.ToInt32(traces) > 0);

            var tests1 = _db.ExecuteScalar($"select count(Ptn_Pk)  from  [DTL_CUSTOMFORM_HIV-Test 1_HTC_Lab_MOH_362] where Ptn_Pk in ({savePatient.Id})");
            Assert.True(Convert.ToInt32(tests1) > 0);

            var tests2 = _db.ExecuteScalar($"select count(Ptn_Pk)  from  [DTL_CUSTOMFORM_HIV-Test 2_HTC_Lab_MOH_362] where Ptn_Pk in ({savePatient.Id})");
            Assert.True(Convert.ToInt32(tests2) > 0);
        }
        [Test]
        public void should_CreateOrUpdate_New_Lab_Detail()
        {
            var savePatient = _patientRepository.Get(_patient.mAfyaId.Value);
            Assert.IsNotNull(savePatient);
            _patientEncounterRepository.CreateOrUpdate(_encounterInfo, subscriberSystem, location);
            Assert.AreEqual(1, _db.ExecuteScalar($"select count(Ptn_Pk)  from  [DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] where Ptn_Pk in ({savePatient.Id})"));
            var dr=_db.ExecuteReader($"select *  from  [DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] where Ptn_Pk in ({savePatient.Id})");

            while (dr.Read())
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(dr["FinalTestOneResult"].ToString()));
                Assert.IsFalse(string.IsNullOrWhiteSpace(dr["FinalResultTestTwo"].ToString()));
                Assert.IsFalse(string.IsNullOrWhiteSpace(dr["finalResultHTS"].ToString()));
                Assert.IsFalse(string.IsNullOrWhiteSpace(dr["FinalResultsGiven"].ToString()));
                Assert.IsFalse(string.IsNullOrWhiteSpace(dr["CoupleDiscordant"].ToString()));
                Assert.IsFalse(string.IsNullOrWhiteSpace(dr["KeyPop"].ToString()));
                Assert.IsFalse(string.IsNullOrWhiteSpace(dr["phoneNumber"].ToString()));
            }
            /*
FinalTestOneResult	DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362
FinalResultTestTwo	DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362
finalResultHTS	DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362
FinalResultsGiven	DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362
CoupleDiscordant	DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362
             */
        }

        [Test]
        public void should_CreateOrUpdate_New_Mulitple_Lab_Detail()
        {
            var savePatient = _patientRepository.Get(_patient.mAfyaId.Value);
            var savePatient2 = _patientRepository.Get(_patient2.mAfyaId.Value);

            Assert.IsNotNull(savePatient);
            Assert.IsNotNull(savePatient2);

            _patientEncounterRepository.CreateOrUpdate(_encounterInfo, subscriberSystem, location);
            _patientEncounterRepository.CreateOrUpdate(_encounterInfo2, subscriberSystem, location);

            Assert.AreEqual(1, _db.ExecuteScalar($"select count(Ptn_Pk)  from  [DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] where Ptn_Pk in ({savePatient.Id})"));
            Assert.AreEqual(1, _db.ExecuteScalar($"select count(Ptn_Pk)  from  [DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] where Ptn_Pk in ({savePatient2.Id})"));

            var dr = _db.ExecuteReader($"select *  from  [DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] where Ptn_Pk in ({savePatient.Id})");
            var dr2 = _db.ExecuteReader($"select *  from  [DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362] where Ptn_Pk in ({savePatient2.Id})");

            while (dr.Read())
            {
                Assert.AreEqual("no comment", dr["Remarks"].ToString());
                Assert.AreEqual(_client.Person.Contacts.First().Phone.ToString(),dr["phoneNumber"].ToString());
                Console.WriteLine($"{_client.Person.Contacts.First().Phone.ToString()} == {dr["phoneNumber"].ToString()}");
            }
            while (dr2.Read())
            {
                Assert.AreEqual("none", dr2["Remarks"].ToString());
                Assert.AreEqual(_client2.Person.Contacts.First().Phone.ToString(), dr2["phoneNumber"].ToString());
                Console.WriteLine($"{_client2.Person.Contacts.First().Phone.ToString()} == {dr2["phoneNumber"].ToString()}");
            }
            /*
FinalTestOneResult	DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362
FinalResultTestTwo	DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362
finalResultHTS	DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362
FinalResultsGiven	DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362
CoupleDiscordant	DTL_FBCUSTOMFIELD_HTC_Lab_MOH_362
             */
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
            delete from  ord_Visit where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  mst_Patient where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  lnk_patientprogramstart where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  dtl_FamilyInfo where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            ");
        }
    }
}
