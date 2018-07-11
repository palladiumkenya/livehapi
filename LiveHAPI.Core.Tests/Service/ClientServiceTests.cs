using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Studio;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.Core.Service;
using LiveHAPI.Infrastructure;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Shared.ValueObject;
using LiveHAPI.Shared.ValueObject.Meta;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Z.Dapper.Plus;
using Action = System.Action;

namespace LiveHAPI.Core.Tests.Service
{
    [TestFixture]
    public class ClientServiceTests
    {
        private LiveHAPIContext _context;
        private IClientService _clientService;
        private List<ClientInfo> _clientInfos;
        private PracticeRepository _practiceRepository;
        private IPersonRepository _personRepository;
        private IClientRepository _clientRepository;

        [OneTimeSetUp]
        public void Init()
        {
            DapperPlusManager.AddLicense("1755;701-ThePalladiumGroup", "9005d618-3965-8877-97a5-7a27cbb21c8f");

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ClientStateInfo, ClientState>();
                cfg.CreateMap<TempClientRelationship, ClientRelationship>();
                cfg.CreateMap<ClientRelationship, TempClientRelationship>();

                cfg.CreateMap<County, CountyInfo>();
                cfg.CreateMap<SubCounty, SubCountyInfo>();

                cfg.CreateMap<Category, CategoryInfo>();
                cfg.CreateMap<Item, ItemInfo>();
                cfg.CreateMap<CategoryItem, CategoryItemInfo>();

                cfg.CreateMap<PracticeType, PracticeTypeInfo>();
                cfg.CreateMap<IdentifierType, IdentifierTypeInfo>();
                cfg.CreateMap<RelationshipType, RelationshipTypeInfo>();
                cfg.CreateMap<KeyPop, KeyPopInfo>();
                cfg.CreateMap<MaritalStatus, MaritalStatusInfo>();
                cfg.CreateMap<ProviderType, ProviderTypeInfo>();
                cfg.CreateMap<Action, ActionInfo>();
                cfg.CreateMap<Condition, ConditionInfo>();
                cfg.CreateMap<ValidatorType, ValidatorTypeInfo>();
                cfg.CreateMap<CategoryItem, CategoryItemInfo>();
                cfg.CreateMap<ConceptType, ConceptTypeInfo>();
                cfg.CreateMap<Validator, ValidatorInfo>();
                cfg.CreateMap<EncounterType, EncounterTypeInfo>();

                cfg.CreateMap<SubscriberCohort, CohortInfo>();

                cfg.CreateMap<Encounter, EncounterInfo>();
                cfg.CreateMap<Obs, ObsInfo>();
                cfg.CreateMap<ObsTestResult, ObsTestResultInfo>();
                cfg.CreateMap<ObsFinalTestResult, ObsFinalTestResultInfo>();
                cfg.CreateMap<ObsTraceResult, ObsTraceResultInfo>();
                cfg.CreateMap<ObsLinkage, ObsLinkageInfo>();
                cfg.CreateMap<ObsMemberScreening, ObsMemberScreeningInfo>();
                cfg.CreateMap<ObsPartnerScreening, ObsPartnerScreeningInfo>();
                cfg.CreateMap<ObsFamilyTraceResult, ObsFamilyTraceResultInfo>();
                cfg.CreateMap<ObsPartnerTraceResult, ObsPartnerTraceResultInfo>();

                cfg.CreateMap<ClientSummaryInfo, ClientSummary>();

                int userId;
                cfg.CreateMap<Core.Model.People.User, UserDTO>()
                    .ForMember(x => x.Password, o => o.MapFrom(s => s.DecryptedPassword))
                    .ForMember(x => x.UserId, o => o.MapFrom(s => int.TryParse(s.SourceRef, out userId) ? userId : 0));

                cfg.CreateMap<Person, PersonDTO>()
                    .ForMember(x => x.FirstName,
                        o => o.MapFrom(s => null != s.Names.FirstOrDefault() ? s.Names.FirstOrDefault().FirstName : ""))
                    .ForMember(x => x.MiddleName,
                        o => o.MapFrom(s =>
                            null != s.Names.FirstOrDefault() ? s.Names.FirstOrDefault().MiddleName : ""))
                    .ForMember(x => x.LastName,
                        o => o.MapFrom(s => null != s.Names.FirstOrDefault() ? s.Names.FirstOrDefault().LastName : ""));
                cfg.CreateMap<Provider, ProviderDTO>();

            });
        }
        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config["connectionStrings:realConnection"];

            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new LiveHAPIContext(options);
            _context.Database.Migrate();
            TestData.Init();
            TestDataCreator.Init(_context);
            _clientInfos = TestData.TestClientInfos();
            _practiceRepository = new PracticeRepository(_context);
            _personRepository = new PersonRepository(_context);
            _clientRepository = new ClientRepository(_context);
            _clientService = new ClientService(_practiceRepository, new PersonRepository(_context),
                new ClientRepository(_context),new InvalidMessageRepository(_context));
        }

        [Test]
        public void should_Sync_New_Create_Person()
        {
            var client = _clientInfos.Last();
            var prac = _practiceRepository.GetByCode(client.PracticeCode);


            _clientService.Sync(prac.Id, client);

            _personRepository = new PersonRepository(_context);
            var savedPerson = _personRepository.Get(client.Person.Id);
            Assert.IsNotNull(savedPerson);
            Assert.IsTrue(savedPerson.Names.Count > 0);
            Assert.IsTrue(savedPerson.Contacts.Count > 0);
            Assert.IsTrue(savedPerson.Addresses.Count > 0);
            Console.WriteLine(savedPerson);

            foreach (var name in savedPerson.Names)
            {
                Console.WriteLine($"  {name}");
            }
            foreach (var address in savedPerson.Addresses)
            {
                Console.WriteLine($"  {address}");
            }
            foreach (var contact in savedPerson.Contacts)
            {
                Console.WriteLine($"  {contact}");
            }
        }


        [Test]
        public void should_Sync_Update_Person()
        {
            var client = _clientInfos.First();            
            var c = _clientRepository.Get(client.Id);
            Assert.AreEqual(c.PersonId,client.Person.Id);

            
            client.Person.FirstName = "MAUN";
            client.Person.LastName = "MAUN L";

            _clientService.Sync(c.PracticeId, client);

            _personRepository = new PersonRepository(_context);
            var savedPerson = _personRepository.Get(client.Person.Id);
            Assert.IsNotNull(savedPerson);
            
            Assert.AreEqual("MAUN", savedPerson.Names.First().FirstName);
            Assert.AreEqual("MAUN L", savedPerson.Names.First().LastName);
            Console.WriteLine(savedPerson);

            foreach (var name in savedPerson.Names)
            {
                Console.WriteLine($"  {name}");
            }
            foreach (var address in savedPerson.Addresses)
            {
                Console.WriteLine($"  {address}");
            }
            foreach (var contact in savedPerson.Contacts)
            {
                Console.WriteLine($"  {contact}");
            }
        }

        [Test]
        public void should_Sync_New_Create_Client()
        {
            var client = _clientInfos.Last();
            var prac = _practiceRepository.GetByCode(client.PracticeCode);

            _clientService.Sync(prac.Id, client);
            _clientRepository = new ClientRepository(_context);

            var savedClient = _clientRepository.Get(client.Id);
            Assert.IsNotNull(savedClient);
            Console.WriteLine(savedClient);
        }

        [Test]
        public void should_Sync_Update_Client()
        {
            var client = _clientInfos.First();
            var c = _clientRepository.Get(client.Id);
            Assert.AreEqual(c.PersonId, client.Person.Id);

            client.KeyPop = "Fala";
            _clientService.Sync(client.PracticeId.Value, client);
            _clientRepository = new ClientRepository(_context);

            var savedClient = _clientRepository.Get(client.Id);
            Assert.IsNotNull(savedClient);
            Assert.AreEqual("Fala", savedClient.KeyPop);
            Console.WriteLine(savedClient);
        }

        [Test]
        public void should_Preserve_Client_With_No_Practices()
        {
            var client = _clientInfos.Last();
            client.PracticeId=Guid.NewGuid();
            _clientService.SmartSync(client);
            _clientRepository = new ClientRepository(_context);

            var savedClient = _clientRepository.Get(client.Id);
            Assert.Null(savedClient);

            var invalidMessages = _context.InvalidMessages.Where(x => x.ClientId == client.Id);
            Assert.True(invalidMessages.Any());
            foreach (var invalidMessage in invalidMessages)
            {
                Console.WriteLine(invalidMessage);
            }
        }
    }
}
