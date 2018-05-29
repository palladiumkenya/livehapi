using System;
using LiveHAPI.Core.Interfaces;
using LiveHAPI.Core.Model.Setting;
using LiveHAPI.Core.Service;
using NUnit.Framework;

namespace LiveHAPI.Core.Tests.Service
{
    [TestFixture]
    public class RestManagerTests
    {
        private readonly string _baseUrl = "http://localhost:3333";
        private IRestManager _reader;

        [SetUp]
        public void Setup()
        {
            _reader = new RestManger();
        }

        [Test]
        public void should_Verify_Url()
        {
            var users = _reader.VerfiyUrl(new Endpoints(_baseUrl)).Result;
            Assert.NotNull(users);
            Console.WriteLine(users.FacilityName);
        }
    }
}