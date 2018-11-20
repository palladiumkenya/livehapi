using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using FizzWare.NBuilder;
using LiveHAPI.Core.Interfaces.Builders;
using LiveHAPI.Core.Model.Builder;
using LiveHAPI.Core.Model.People;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace LiveHAPI.Core.Tests.Builder
{
    [TestFixture]
    public class ClientContactNetworkBuilderTests
    {
        private IClientContactNetworkBuilder _builder;
        private Contact _primary;
        private List<Contact> _secondaryContacts;

        [SetUp]
        public void SetUp()
        {
            _primary = Builder<Contact>.CreateNew().Build();
            _primary.FirstName = "Index";
            _secondaryContacts=Builder<Contact>.CreateListOfSize(2).Build().ToList();
            _secondaryContacts[0].FirstName = "Secondary 1";
            _secondaryContacts[1].FirstName = "Secondary 2";
        }
        [Test]
        public void should_Build()
        {
            _builder=new ClientContactNetworkBuilder();
            _builder.CreatePrimary(_primary);
            _builder.AddSecondaryContact(_secondaryContacts.First());
            _builder.AddSecondaryContacts(_secondaryContacts);
            var network = _builder.Build().ToList();
            var primaryId = network.First(x => x.ClientId == _primary.Id).Id;
            Assert.True(network.Any());
            Assert.False(network.Any(x=>null!=x.ClientContactNetworkId && x.ClientContactNetworkId!=primaryId));
            
            network.ForEach(Console.WriteLine);
}
    }
}