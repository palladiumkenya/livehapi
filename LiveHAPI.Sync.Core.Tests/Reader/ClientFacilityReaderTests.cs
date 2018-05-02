using System;
using System.Linq;
using System.Net.Http;
using LiveHAPI.Sync.Core.Interface;
using LiveHAPI.Sync.Core.Reader;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests.Reader
{
    [TestFixture]
    public class ClientFacilityReaderTests
    {
        private readonly string _baseUrl = "http://localhost:3333";
        private HttpClient _httpClient;
        private IClientFacilityReader _reader;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _reader = new ClientFacilityReader(_httpClient);
        }

        [Test]
        public void should_Read_Facilitys()
        {
            var users = _reader.Read().Result.ToList();
            Assert.True(users.Any());
            foreach (var clientFacility in users)
            {
                Console.WriteLine(clientFacility);
            }
        }
    }
}