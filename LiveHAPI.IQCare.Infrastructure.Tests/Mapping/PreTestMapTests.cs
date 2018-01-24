using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Subscriber;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using NUnit.Framework;

namespace LiveHAPI.IQCare.Infrastructure.Tests.Mapping
{
    [TestFixture]
    public class PreTestMapTests
    {
        private SqlConnection _hapiConnection;
        private SqlConnection _emrConnection;

        private readonly string _sqlPretestMap =PreTestMap.GetQuery();
        private readonly string _sqlPretestBindMap = PreTestBindMap.GetQuery();

        private List<Question> _allQuestions;
        private List<PreTestMap> _preTestMaps;
        private List<PreTestBindMap> _preTestBindMaps;
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

            _allQuestions = _hapiConnection.GetAll<Question>().Where(x => !x.Fact.Equals("alien")).ToList();
            _preTestMaps = _hapiConnection.Query<PreTestMap>(_sqlPretestMap).ToList();
            _preTestBindMaps = _hapiConnection.Query<PreTestBindMap>(_sqlPretestBindMap).ToList();
        }

        [SetUp]
        public void SetUp()
        {
            _hapiConnection = new SqlConnection(_config["connectionStrings:hAPIConnection"]);
            _emrConnection = new SqlConnection(_config["connectionStrings:EMRConnection"]);
        }

        [Test]
        public void should_map_non_alien()
        {
            Assert.True(_allQuestions.Count > 0);
            Assert.True(_preTestMaps.Count > 0);
            Assert.AreEqual(_allQuestions.Count,_preTestMaps.Count);
            Console.WriteLine($"LiveHTS:{_allQuestions.Count}:EMR:{_preTestMaps.Count}");
        }

        [Test]
        public void should_map_to_valid_destination()
        {
            Assert.True(_preTestMaps.Count > 0);
            foreach (var preTestMap in _preTestMaps)
            {
                Assert.DoesNotThrow(() =>
                {
                    var count = _emrConnection.ExecuteScalar<int>(preTestMap.SqlColumn());

                });
            }

            foreach (var preTestMap in _preTestMaps)
            {
                Console.WriteLine(preTestMap.Info());
            }
        }

        [Test]
        public void should_have_mapped_lookups()
        {
            Assert.True(_preTestBindMaps.Count > 0);

            foreach (var p in _preTestBindMaps)
            {
                var subs = _hapiConnection.GetAll<SubscriberTranslation>()
                    .Where(x => x.Ref.Trim().ToLower() == p.Field.Trim().ToLower()).ToList();
                Assert.AreEqual(p.SubField.Trim().ToLower(),p.Iqfield.Trim().ToLower());
                Assert.True(subs.Count>0);
                Console.WriteLine($"        {p.Display}");
                foreach (var subscriberTranslation in subs)
                {
                    Console.WriteLine();
                    Console.WriteLine($"   {subscriberTranslation.Code} | {subscriberTranslation.Display} >> {subscriberTranslation.SubCode} | {subscriberTranslation.SubDisplay}");
                }

                Console.WriteLine(new string('_',30));
            }
        }

        [Test]
        public void should_have_valid_lookups()
        {
            Assert.True(_preTestBindMaps.Count > 0);

            foreach (var p in _preTestBindMaps)
            {
                Console.WriteLine($"Lookups   |   {p.Display}");
                var mappedLookups = _hapiConnection.GetAll<SubscriberTranslation>()
                    .Where(x => x.Ref.Trim().ToLower() == p.Field.Trim().ToLower()).ToList();
                Assert.True(mappedLookups.Count > 0);

                var lookups = _emrConnection.Query<Lookup>(p.GetLookups()).ToList();
                Assert.True(lookups.Count > 0);

                foreach (var mappedLookup in mappedLookups)
                {
                    Console.WriteLine($"{mappedLookup.SubDisplay}");

                    var correctMap = lookups.Where(
                        x => x.DeleteFlag.HasValue &&
                             x.DeleteFlag.Value == 0 &&
                             x.Name.ToLower().Trim() == mappedLookup.SubDisplay.ToLower().Trim()).ToList();

                    Assert.True(correctMap.Count == 1);

                    if (p.IsYesNo())
                    {
                        if (mappedLookup.SubCode == "1")
                        {
                            Assert.AreEqual(
                                mappedLookup.SubCode.ToLower().Trim(),
                                correctMap.First().Id.ToString().ToLower().Trim());
                        }
                    }
                    else
                    {
                        Assert.AreEqual(
                            mappedLookup.SubCode.ToLower().Trim(),
                            correctMap.First().Id.ToString().ToLower().Trim());
                    }

                    Console.WriteLine($"{mappedLookup.Display} >> {mappedLookup.SubDisplay}");
                }

                Console.WriteLine(new string('_', 30));
                Console.WriteLine();
            }
        }

        [TearDown]
        public void should_Get_VisitTypes()
        {
           _emrConnection.Dispose();
            _hapiConnection.Dispose();
        }
    }
}