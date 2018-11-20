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
    public class ClientNetworkBuilderTests
    {
        private IClientNetworkBuilder _builder;
        private Contact _primary;
        private List<Contact> _secondaryContacts;

        [SetUp]
        public void SetUp()
        {
            _primary = Builder<Contact>.CreateNew().Build();
            _secondaryContacts=Builder<Contact>.CreateListOfSize(3).Build().ToList();
        }
        [Test]
        public void should_Build()
        {
            _builder=new ClientNetworkBuilder();
            _builder.CreatePrimary(_primary);
            _builder.AddSecondaryContact(_secondaryContacts.First());
            _builder.AddSecondaryContacts(_secondaryContacts);
            var network = _builder.Build().ToList();
            Assert.True(network.Any());
            
            network.ForEach(Console.WriteLine);
        }
    }
}