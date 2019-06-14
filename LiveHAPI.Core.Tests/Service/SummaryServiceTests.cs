using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;
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
using Action = System.Action;

namespace LiveHAPI.Core.Tests.Service
{
    [TestFixture]
    public class SummaryServiceTests
    {
        private LiveHAPIContext _context;
        private IClientService _clientService;
        private List<ClientInfo> _clientInfos;
        private PracticeRepository _practiceRepository;
        private IPersonRepository _personRepository;
        private IClientRepository _clientRepository;
        private ISummaryService _summaryService;
        private ISyncManagerService _syncManagerService;

        [OneTimeSetUp]
        public void Init()
        {
             AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PracticeDTO, Practice>();
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

                cfg.CreateMap<ClientContactNetwork, ContactTreeInfo>()
                    .ForMember(x => x.Label, o => o.MapFrom(s => s.Names))
                    .ForMember(x => x.Children, o => o.MapFrom(s => s.Networks));

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

            _practiceRepository = new PracticeRepository(_context);
            _personRepository = new PersonRepository(_context);
            _clientRepository = new ClientRepository(_context);
            _clientService = new ClientService(_practiceRepository, new PersonRepository(_context),
                new ClientRepository(_context), new InvalidMessageRepository(_context));
            _syncManagerService=new SyncManagerService(new ClientStageRepository(_context));
            _summaryService=new SummaryService(new ItemRepository(_context),new ClientSummaryRepository(_context),new UserSummaryRepository(_context),new EncounterRepository(_context),_syncManagerService );
        }

        [Test]
        public void should_Generate_Client_Summary()
        {
            /*
                50E56B13-BBB4-4E62-AA48-A896013F79C7
                045F0947-CD80-4E82-8E79-A896013FE740
             */

            var personMatches = _clientService.FindById(new Guid("045F0947-CD80-4E82-8E79-A896013FE740"));
            Assert.NotNull(personMatches);
            var client = personMatches.FirstOrDefault().Person.Clients.FirstOrDefault();
            Assert.NotNull(client);

            var summary = _summaryService.Generate(client).ToList();
            Assert.True(summary.Count>0);
            foreach (var clientSummary in summary)
            {
                Console.WriteLine(clientSummary);
            }
        }

        [Test]
        public void should_Generate_User_Summary()
        {
            /*
                61A9E04C-2ED0-414A-9387-A7B7016DF233
            */

            var summaries = _summaryService.Generate(new Guid("61A9E04C-2ED0-414A-9387-A7B7016DF233")).ToList();
            Assert.True(summaries.Count > 0);
            foreach (var userSummary in summaries)
            {
                Console.WriteLine(userSummary);
            }
        }
    }
}
