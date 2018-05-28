using System;
using LiveHAPI.Core.Interfaces;
using LiveHAPI.Core.Model.Setting;
using NUnit.Framework;

namespace LiveHAPI.Infrastructure.Tests
{
    [TestFixture]
    public class DbManagerTests
    {
        private readonly string _connection = @"Data Source=.\\KOSKE14;Initial Catalog=LiveHAPI;Persist Security Info=True;User ID=sa;Password=maun;MultipleActiveResultSets=True;Pooling=True";
        private IDbManager _dbManager;
        private DbProtocol _dbProtocol;

        [SetUp]
        public void SetUp()
        {
            _dbManager=new DbManager();
            _dbProtocol=new DbProtocol();
            _dbProtocol.Database = "iqcare";
            _dbProtocol.Password = "c0nstella";
            _dbProtocol.User = "sa";
            _dbProtocol.Server = @"192.168.1.10\sqlexpress";
        }

        [Test]
        public void should_Verify_Connection()
        {
            _dbProtocol.Server = @".\KOSKE14";
            _dbProtocol.User = "sa";
            _dbProtocol.Password = "maun";
            _dbProtocol.Database = "LiveHAPI";
            var con = _dbManager.Verfiy(_dbProtocol);
            Assert.True(con);
        }

        [Test]
        public void should_Build_Connection()
        {
            var con = _dbManager.BuildConncetion(_dbProtocol);
            Assert.False(string.IsNullOrWhiteSpace(con));
            Assert.AreEqual(@"Data Source=192.168.1.10\sqlexpress;Initial Catalog=iqcare;Persist Security Info=True;User ID=sa;Password=c0nstella;Pooling=True;MultipleActiveResultSets=True", con);
            Console.WriteLine(con);
        }

        [Test]
        public void should_Generate_Connection()
        {
            var con = _dbManager.GetConncetion(_connection,_dbProtocol);
            Assert.False(string.IsNullOrWhiteSpace(con));
            Assert.AreEqual(@"Data Source=192.168.1.10\sqlexpress;Initial Catalog=iqcare;Persist Security Info=True;User ID=sa;Password=c0nstella;Pooling=True;MultipleActiveResultSets=True",con);
            Console.WriteLine(con);
        }
    }
}