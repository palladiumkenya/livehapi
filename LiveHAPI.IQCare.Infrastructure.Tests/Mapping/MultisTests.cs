using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Subscriber;
using LiveHAPI.IQCare.Infrastructure.Tests.Maps;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using NUnit.Framework;

namespace LiveHAPI.IQCare.Infrastructure.Tests.Mapping
{
    [TestFixture]
    public class MultisTests
    {
        private SqlConnection _hapiConnection;
        private SqlConnection _emrConnection;

        private List<SubscriberMap> _allMultis;
        private List<FormSectionMap> _allSectionMaps;
        private IConfigurationRoot _config;

        [OneTimeSetUp]
        public void Init()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _hapiConnection = new SqlConnection(_config["connectionStrings:hAPIConnection"]);
            _emrConnection = new SqlConnection(_config["connectionStrings:EMRConnection"]);
            string script = File.ReadAllText(@"htchapi001.sql");

            SqlConnection conn = new SqlConnection(_emrConnection.ConnectionString);
            Server server = new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery(script);


            _allMultis = _hapiConnection.GetAll<SubscriberMap>().Where(
                x => x.Mode.ToLower().Trim() == "Multi".ToLower().Trim()
            ).ToList();

            _allSectionMaps = _emrConnection.Query<FormSectionMap>(FormSectionMap.GetQuery()).ToList();
        }

        [SetUp]
        public void SetUp()
        {
            _hapiConnection = new SqlConnection(_config["connectionStrings:hAPIConnection"]);
            _emrConnection = new SqlConnection(_config["connectionStrings:EMRConnection"]);
        }

        [Test]
        public void should_have_form_section_ids()
        {
            Assert.True(_allMultis.Count > 0);

            foreach (var subscriberMap in _allMultis)
            {
                Assert.NotNull(string.IsNullOrWhiteSpace(subscriberMap.SectionId));
                Assert.False(string.IsNullOrWhiteSpace(subscriberMap.FormId));
                Assert.NotNull(_allSectionMaps.FirstOrDefault(x=>x.FeatureId==Convert.ToInt32(subscriberMap.FormId) && x.SectionId== Convert.ToInt32(subscriberMap.SectionId)));
            }
        }
        [Test]
        public void should_have_form_section_ids_int_dest()
        {
            string sql = "select top 1 SectionId,FormID from ";
            Assert.True(_allMultis.Count > 0);

            foreach (var subscriberMap in _allMultis)
            {
              
                Assert.DoesNotThrow(() => { _emrConnection.Execute($"{sql} {subscriberMap.SubName}");});
            }
        }
        
        [TearDown]
        public void TearDown()
        {
            _emrConnection.Dispose();
            _hapiConnection.Dispose();
        }
    }
}