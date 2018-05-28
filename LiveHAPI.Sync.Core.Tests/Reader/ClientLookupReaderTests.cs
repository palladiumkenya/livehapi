using System;
using System.Linq;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Reader;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Reader
{
    [TestFixture]
    public class ClientLookupReaderTests
    {
        private readonly string _baseUrl = "http://192.168.1.217/iqcareapi";
        private IClientLookupReader _reader;

        [SetUp]
        public void Setup()
        {
            _reader = new ClientLookupReader(new RestClient(_baseUrl));
        }

        [Test]
        public void should_Read_Lookups()
        {
            var users = _reader.Read().Result.ToList();
            Assert.True(users.Any());
            foreach (var clientLookup in users)
            {
                Assert.True(!string.IsNullOrWhiteSpace(clientLookup.ItemName));
                Console.WriteLine(clientLookup);
            }
        }
    }
}