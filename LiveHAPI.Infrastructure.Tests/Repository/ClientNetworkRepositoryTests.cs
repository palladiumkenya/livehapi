using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Model.Exchange;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Infrastructure.Repository;
using LiveHAPI.Shared.Custom;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LiveHAPI.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class ClientNetworkRepositoryTests
    {
        private List<ClientStage> _clients;
        private List<ClientStageRelationship> _relationships;
        private LiveHAPIContext _context;
        private IClientNetworkRepository _repository;
        
        [OneTimeSetUp]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseInMemoryDatabase(databaseName: LiveGuid.NewGuid().ToString())
                .Options;

            _context =new LiveHAPIContext(options);
            
            _clients[0].FirstName = "Index0";
            _clients[1].FirstName = "Index0-Contact1";
            _clients[2].FirstName = "Index0-Contact2";
            _clients[3].FirstName = "Index0-Contact1-Contact1";
            _clients[4].FirstName = "Index0-Contact1-Contact2";
            
            _relationships = Builder<ClientStageRelationship>.CreateListOfSize(3)
                .All()
                .With(x=>x.Relation=1)
                .With(x=>x.IsPartner=true)
                .With(x=>x.RelationName="Partner")
                .Build().ToList();
            
            // I0 -I0C1
            _relationships[0].IndexClientId = _clients[0].ClientId;
            _relationships[0].SecondaryClientId = _clients[1].ClientId;
            
            // I0 -I0C2
            _relationships[0].IndexClientId = _clients[0].ClientId;
            _relationships[0].SecondaryClientId = _clients[2].ClientId;
            
            // I0C1 - I0C1.C1
            _relationships[0].IndexClientId = _clients[1].ClientId;
            _relationships[0].SecondaryClientId = _clients[3].ClientId;
            
            // I0C1 - I0C1.C2
            _relationships[0].IndexClientId = _clients[1].ClientId;
            _relationships[0].SecondaryClientId = _clients[4].ClientId;
            
            _context.AddRange(_clients);
            _context.AddRange(_relationships);
            _context.SaveChanges();

        }
        [SetUp]
        public void Setup()
        {
            _repository=new ClientNetworkRepository(_context);
        }
        
        [Test]
        public void should_Generate()
        {
            _repository.Generate().Wait();

            var networks = _repository.GetAll().ToList();
            Assert.True(networks.Any());
        }
    }
}