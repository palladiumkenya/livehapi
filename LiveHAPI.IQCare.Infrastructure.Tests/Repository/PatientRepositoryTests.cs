using System;
using System.Data.Common;
using System.Data.SqlClient;
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
    public class PatientRepositoryTests
    {
        private EMRContext  _context;
        private IPatientRepository _patientRepository;
        private IPatientFamilyRepository _patientFamilyRepository;
        private IConfigRepository _configRepository;
        private ISubscriberSystemRepository _subscriberSystemRepository;
        private Patient _patient, _patientPartner;
        private SubscriberSystem _subscriberSystem;
        private Location _location;
        private ClientInfo _client, _clientPartner;
        private DbConnection _db;
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
            _patientRepository=new PatientRepository(_context);
            _patientFamilyRepository=new PatientFamilyRepository(_context);
            _client = TestData.TestClientInfo();
            _clientPartner = TestData.TestClientInfo2();
            _patient = Patient.Create(_client, _location.FacilityID, _subscriberSystem);
            _patientPartner = Patient.Create(_clientPartner, _location.FacilityID, _subscriberSystem);
            _db = _context.Database.GetDbConnection();
        }
        
        [Test]
        public void should_CreateOrUpdate_New()
        {
            _patientRepository.CreateOrUpdate(_patient,_subscriberSystem,_location);
            var savePatient = _patientRepository.Get(_patient.mAfyaId.Value);
            Assert.IsNotNull(savePatient);
            Assert.AreEqual("201707001", savePatient.HTSID);
            Assert.True(savePatient.Id > -1);
            Console.WriteLine($"Patient: {savePatient}");

            var regVisitTypeId = _subscriberSystem.Configs.First(x => x.Area == "Registration" && x.Name == "VisitTypeId").Value;
            var htsVisitTypeId = _subscriberSystem.Configs.First(x => x.Area == "HTS" && x.Name == "Enrollment.VisitTypeId").Value;
            var moduleId = _subscriberSystem.Configs.First(x => x.Area == "HTS" && x.Name == "ModuleId").Value;

            Assert.AreEqual(1,_db.ExecuteScalar($"select count(Ptn_Pk)  from  [mst_Patient] where Ptn_Pk in ({savePatient.Id})"));
            Assert.True(_db.ExecuteScalar($"SELECT IQNumber FROM mst_Patient WHERE Ptn_Pk = {savePatient.Id}").ToString().Contains("IQ-"));
            Assert.AreEqual(1,_db.ExecuteScalar($"select count(Ptn_Pk)  from  [ord_Visit] where Ptn_Pk in ({savePatient.Id}) AND VisitType={regVisitTypeId}"));
            Assert.AreEqual(1, _db.ExecuteScalar($"select count(Ptn_Pk)  from  [ord_Visit] where Ptn_Pk in ({savePatient.Id}) AND VisitType={htsVisitTypeId}"));
            Assert.AreEqual(1, _db.ExecuteScalar($"select count(Ptn_Pk)  from  [lnk_patientprogramstart] where Ptn_Pk in ({savePatient.Id}) AND ModuleID={moduleId}"));
        }

        [Test]
        public void should_CreateOrUpdate_New_With_Index_Relations()
        {
            _patientRepository.CreateOrUpdate(_patient, _subscriberSystem, _location);
            var savePatient = _patientRepository.Get(_patient.mAfyaId.Value);
            Assert.IsNotNull(savePatient);
            _patientRepository.CreateOrUpdateRelations(_client.Id, _client.Relationships, _subscriberSystem, _location);
            
            _patientRepository.CreateOrUpdate(_patientPartner, _subscriberSystem, _location);
            var savePatientPartner = _patientRepository.Get(_patientPartner.mAfyaId.Value);
            Assert.IsNotNull(savePatientPartner);
            _patientRepository.CreateOrUpdateRelations(_clientPartner.Id, _clientPartner.Relationships, _subscriberSystem, _location);

            var indexRelations = _patientFamilyRepository.GetMembers(savePatient.Id).ToList();
            Assert.True(indexRelations.Count > 0);

            /*
            Assert.AreEqual(savePatientPartner.Id, indexRelations.First().ReferenceId);

//            var partnerRelations = _patientFamilyRepository.GetMembers(savePatientPartner.Id).ToList();
//            Assert.True(partnerRelations.Count > 0);
//            Assert.AreEqual(savePatient.Id, partnerRelations.First().ReferenceId);

            Console.WriteLine($"Index:{savePatient}");
            foreach (var r in indexRelations)
            {
                Console.WriteLine($"{r}");
            }
            */
        }

        [Test]
        public void should_CreateOrUpdate_Update()
        {
            _patientRepository.CreateOrUpdate(_patient, _subscriberSystem, _location);
            var savePatient = _patientRepository.Get(_patient.mAfyaId.Value);
            Assert.IsNotNull(savePatient);

            _patient.FirstName = "Maun";
            _patient.MiddleName = "Maun";
            _patient.LastName = "Maun";
            _patient.HTSID = "XXX";

            _patientRepository.CreateOrUpdate(_patient, _subscriberSystem, _location);
            _patientRepository=new PatientRepository(_context);
            var updatedPatient = _patientRepository.Get(_patient.mAfyaId.Value);

            Assert.IsNotNull(updatedPatient);
            Assert.AreEqual("Maun", updatedPatient.FirstName);
            Assert.AreEqual("Maun", updatedPatient.MiddleName);
            Assert.AreEqual("Maun", updatedPatient.LastName);
            Assert.AreEqual("XXX", updatedPatient.HTSID);
            
            Console.WriteLine($"Patient: {updatedPatient}");
        }


        [TearDown]
        public void TearDown()
        {
            var connection = _context.Database.GetDbConnection() as SqlConnection;
            connection.Execute(@"
            delete from  dtl_PatientContacts where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  [DTL_PATIENTHOUSEHOLDINFO] where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
            delete from  [DTL_RURALRESIDENCE] where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
            delete from  [DTL_URBANRESIDENCE]  where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');	
            delete from  [DTL_PATIENTHIVPREVCAREENROLLMENT]  where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');			
            delete from  lnk_patientprogramstart where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');            
            delete from  ord_Visit where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  dtl_FamilyInfo where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            delete from  mst_Patient where Ptn_Pk in (SELECT Ptn_Pk FROM IQCare.dbo.mst_Patient WHERE mAfyaId like '4700b0e0%');
            ");
        }
    }
} 