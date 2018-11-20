using System;
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
    public class ClientContactNetworkRepositoryTests
    {
        private List<ClientStage> _clients;
        private List<ClientStageRelationship> _relationships;
        private LiveHAPIContext _context;
        private IClientContactNetworkRepository _repository;
        
        [OneTimeSetUp]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<LiveHAPIContext>()
                .UseInMemoryDatabase(databaseName: LiveGuid.NewGuid().ToString())
                .Options;

            _context =new LiveHAPIContext(options);

            _clients = Builder<ClientStage>.CreateListOfSize(9).Build().ToList();

            _clients[0].FirstName = "I0";_clients[0].Serial = "0000";
            
            _clients[1].FirstName = "I0C1";  _clients[1].Serial = "1001";
            _clients[2].FirstName = "I0C2";  _clients[2].Serial = "1002";
            
            _clients[3].FirstName = "I0C1 SC1";  _clients[3].Serial = "2001";
            _clients[4].FirstName = "I0C1 SC2";  _clients[4].Serial = "2002";
            
            _clients[5].FirstName = "I0C1 SC2 G1";  _clients[5].Serial = "3001";
            _clients[6].FirstName = "I0C1 SC2 G2";  _clients[6].Serial = "3002";
            _clients[7].FirstName = "I0C1 SC2 G3";  _clients[7].Serial = "3003";
            
            _clients[8].FirstName = "I0C1 SC1 G1";  _clients[7].Serial = "3004";
            
            _relationships = Builder<ClientStageRelationship>.CreateListOfSize(8)
                .All()
                .With(x=>x.Relation=1)
                .With(x=>x.IsPartner=true)
                .With(x=>x.RelationName="Partner")
                .Build().ToList();
            
            // I0C1                               0  01
            _relationships[0].IndexClientId = _clients[0].ClientId;
            _relationships[0].SecondaryClientId = _clients[1].ClientId;
            
            // I0C2                               0 02
            _relationships[1].IndexClientId = _clients[0].ClientId;
            _relationships[1].SecondaryClientId = _clients[2].ClientId;
            
            // I0C1 SC1                           1 13
            _relationships[2].IndexClientId = _clients[1].ClientId;
            _relationships[2].SecondaryClientId = _clients[3].ClientId;
            
            // I0C1 SC2                           1 14
            _relationships[3].IndexClientId = _clients[1].ClientId;
            _relationships[3].SecondaryClientId = _clients[4].ClientId;
            
            // I0C1 SC2 G1                        2 25
            _relationships[4].IndexClientId = _clients[4].ClientId;
            _relationships[4].SecondaryClientId = _clients[5].ClientId;
            
            // I0C1 SC2 G2                        2 26
            _relationships[5].IndexClientId = _clients[4].ClientId;
            _relationships[5].SecondaryClientId = _clients[6].ClientId;
            
            // I0C1 SC2 G3                        2 27
            _relationships[6].IndexClientId = _clients[4].ClientId;
            _relationships[6].SecondaryClientId = _clients[7].ClientId;

            // I0C1 SC1 G1                        2 18
            _relationships[7].IndexClientId = _clients[3].ClientId;
            _relationships[7].SecondaryClientId = _clients[8].ClientId;
            
            _context.AddRange(_clients);
            _context.AddRange(_relationships);
            _context.SaveChanges();

        }
        [SetUp]
        public void Setup()
        {
            _repository=new ClientContactNetworkRepository(_context);
        }
        
        [Test]
        public void should_Generate()
        {
            _repository.Generate().Wait();
            var networks = _repository.LoadAll().ToList();
            Assert.True(networks.Any());

            foreach (var network in networks.Where(x=>x.IsPrimary))
            {
                Console.WriteLine(network);
                foreach (var networkNetwork in network.Networks)
                {
                    Console.WriteLine($" > {networkNetwork}");
                }
            }
          
        }
    }
}