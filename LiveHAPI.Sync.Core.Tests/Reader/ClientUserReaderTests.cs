using System;
using System.Linq;
using LiveHAPI.Sync.Core.Interface.Readers;
using LiveHAPI.Sync.Core.Reader;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Reader
{
    [TestFixture]
    public class ClientUserReaderTests
    {
        private readonly string _baseUrl = "http://192.168.1.217/iqcareapi";
        private IClientUserReader _reader;

        [SetUp]
        public void Setup()
        {
            _reader = new ClientUserReader(new RestClient(_baseUrl));
        }

        [Test]
        public void should_Read_Users()
        {
            var users = _reader.Read().Result.ToList();
            Assert.True(users.Any());
            foreach (var clientUser in users)
            {
                Console.WriteLine(clientUser);
            }
        }
    }
}