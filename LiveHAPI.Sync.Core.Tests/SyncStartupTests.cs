using System;
using LiveHAPI.Sync.Core.Interface;
using NUnit.Framework;

namespace LiveHAPI.Sync.Core.Tests
{
    [TestFixture]
    public class SyncStartupTests
    {
        private readonly string _baseUrl = "http://localhost:4700";
        private IStartup _reader;

        [SetUp]
        public void Setup()
        {
            _reader = new SyncStartup(_baseUrl);
        }

        [Test]
        public void should_Read_Hapi()
        {
            var users = _reader.LoadSettings().Result;
            Assert.NotNull(users);
            Console.WriteLine(users);
        }
    }
}