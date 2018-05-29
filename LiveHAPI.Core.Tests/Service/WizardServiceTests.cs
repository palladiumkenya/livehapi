using System;
using System.Configuration;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Setting;
using LiveHAPI.Core.Service;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.Core.Tests.Service
{
    [TestFixture]
    public class WizardServiceTests
    {
        private IWizardService _service;

        [SetUp]
        public void SetUp()
        {
            var hapiconfig = new ConfigurationBuilder()
                .AddJsonFile(@"apps\hapi\appSettings.json").Build();

            var syncconfig = new ConfigurationBuilder()
                .AddJsonFile(@"apps\sync\appSettings.json").Build();

           // _service = new WizardService(hapiconfig,syncconfig);
        }
        
        [Test]
        public void should_Read_Endpoint()
        {
            var endpoints = _service.ReadEndpoints();
            Assert.NotNull(endpoints);
            Console.WriteLine(endpoints.Iqcare);
        }

        [Test]
        public void should_Read_Connection()
        {
            var connectionStrings = _service.ReadConnectionStrings();
            Assert.NotNull(connectionStrings);
            Console.WriteLine(connectionStrings.HapiConnection);
            Console.WriteLine(connectionStrings.EmrConnection);
        }

        [Test]
        public void should_Read_Sync_Connection()
        {
            var connectionStrings = _service.ReadSyncConnectionStrings();
            Assert.NotNull(connectionStrings);
            Console.WriteLine(connectionStrings.HapiConnection);
            Console.WriteLine(connectionStrings.EmrConnection);

        }

        [Test]
        public void should_UpdateSyncUrl()
        {
            var updated = _service.UpdateSyncUrl(new RestProtocol());
            Assert.True(updated);
        }

        [Test]
        public void should_ValidateDatabase()
        {
            var validated = _service.ValidateDatabase();
            Assert.True(validated);
        }

        [Test]
        public void should_ValidateUrl()
        {
            var validated = _service.ValidateUrl();
            Assert.True(validated);
        }
    }
}