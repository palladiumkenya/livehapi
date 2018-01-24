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
    public class FamilyTraceMapTests
    {
        private SqlConnection _hapiConnection;
        private SqlConnection _emrConnection;

        private readonly string _sqlFamilyTraceMap = FamilyTraceMap.GetQuery();
        private readonly string _sqlFamilyTraceBindMap = FamilyTraceBindMap.GetQuery();

        private List<SubscriberMap> _allFamilyTraceQuestions;
        private List<FamilyTraceMap> _familytraceMaps;
        private List<FamilyTraceBindMap> _familytraceBindMaps;
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

            _allFamilyTraceQuestions = _hapiConnection.GetAll<SubscriberMap>().Where(
                x => x.Name.ToLower().Trim() == "ObsFamilyTraceResult".ToLower().Trim()).ToList();

            _familytraceMaps = _hapiConnection.Query<FamilyTraceMap>(_sqlFamilyTraceMap).ToList();
            _familytraceBindMaps = _hapiConnection.Query<FamilyTraceBindMap>(_sqlFamilyTraceBindMap).ToList();
        }

        [SetUp]
        public void SetUp()
        {
            _hapiConnection = new SqlConnection(_config["connectionStrings:hAPIConnection"]);
            _emrConnection = new SqlConnection(_config["connectionStrings:EMRConnection"]);
        }

        [Test]
        public void should_map_familytrace_non_alien()
        {
            Assert.True(_allFamilyTraceQuestions.Count > 0);
            Assert.True(_familytraceMaps.Count > 0);
            Assert.AreEqual(_allFamilyTraceQuestions.Count, _familytraceMaps.Count);
            Console.WriteLine(
                $"LiveHTS FamilyTrace:{_allFamilyTraceQuestions.Count}:EMR:{_familytraceMaps.Count}");
        }

        [Test]
        public void should_map_familytrace_to_valid_destination()
        {
            Assert.True(_familytraceMaps.Count > 0);

            foreach (var familytraceMap in _familytraceMaps)
            {
                Assert.DoesNotThrow(() =>
                {
                    var count = _emrConnection.ExecuteScalar<int>(familytraceMap.SqlColumn());

                });
            }

            foreach (var familytraceMap in _familytraceMaps)
            {
                Console.WriteLine(familytraceMap.Info());
            }
        }

        [Test]
        public void should_have_mapped_familytrace_lookups()
        {
            Assert.True(_familytraceBindMaps.Count > 0);

            foreach (var p in _familytraceBindMaps)
            {
                var subs = _hapiConnection.GetAll<SubscriberTranslation>()
                    .Where(x => x.Ref.Trim().ToLower() == p.TranslationField.Trim().ToLower()).ToList();
                Assert.AreEqual(p.SubField.Trim().ToLower(), p.Iqfield.Trim().ToLower());
                Assert.True(subs.Count > 0);
                Console.WriteLine($"        {p.Display}");
                foreach (var subscriberTranslation in subs)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        $"   {subscriberTranslation.Code} | {subscriberTranslation.Display} >> {subscriberTranslation.SubCode} | {subscriberTranslation.SubDisplay}");
                }

                Console.WriteLine(new string('_', 30));
            }
        }

        [Test]
        public void should_have_valid_familytrace_lookups()
        {
            Assert.True(_familytraceBindMaps.Count > 0);

            foreach (var p in _familytraceBindMaps)
            {
                Console.WriteLine($"Lookups   |   {p.Display}");
                var mappedLookups = _hapiConnection.GetAll<SubscriberTranslation>()
                    .Where(
                        x => x.Ref.Trim().ToLower() == p.TranslationField.Trim().ToLower()).ToList();
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
        public void TearDown()
        {
            _emrConnection.Dispose();
            _hapiConnection.Dispose();
        }
    }
}