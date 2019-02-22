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
        private readonly string _baseUrl = "http://192.168.1.13:81/iqcareapi";
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

        [Test]
        public void should_Read_EmrVersion()
        {
            var emr = _reader.ReadEmr(new Endpoints(_baseUrl)).Result;
            Assert.NotNull(emr);
            Console.WriteLine(emr.VersionName);
        }
    }
}
