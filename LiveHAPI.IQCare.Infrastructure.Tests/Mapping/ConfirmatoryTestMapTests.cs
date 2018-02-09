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
    public class ConfirmatoryTestMapTests
    {
        private SqlConnection _hapiConnection;
        private SqlConnection _emrConnection;

        private readonly string _sqlConfirmatorytestMap = ConfirmatoryTestMap.GetQuery();
        private readonly string _sqlConfirmatorytestBindMap = ConfirmatoryTestBindMap.GetQuery();

        private List<SubscriberMap> _allConfirmatoryTestQuestions;
        private List<ConfirmatoryTestMap> _confirmatoryTestMaps;
        private List<ConfirmatoryTestBindMap> _confirmatoryTestBindMaps;
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

            _allConfirmatoryTestQuestions = _hapiConnection.GetAll<SubscriberMap>().Where(
                x => x.Name.ToLower().Trim() == "ObsTestResult".ToLower().Trim() &&
                     x.Group == 4
            ).ToList();

            _confirmatoryTestMaps = _hapiConnection.Query<ConfirmatoryTestMap>(_sqlConfirmatorytestMap).ToList();
            _confirmatoryTestBindMaps = _hapiConnection.Query<ConfirmatoryTestBindMap>(_sqlConfirmatorytestBindMap).ToList();
        }

        [SetUp]
        public void SetUp()
        {
            _hapiConnection = new SqlConnection(_config["connectionStrings:hAPIConnection"]);
            _emrConnection = new SqlConnection(_config["connectionStrings:EMRConnection"]);
        }

        [Test]
        public void should_map_confirmatory_test_non_alien()
        {
            Assert.True(_allConfirmatoryTestQuestions.Count > 0);
            Assert.True(_confirmatoryTestMaps.Count > 0);
            Assert.AreEqual(_allConfirmatoryTestQuestions.Count, _confirmatoryTestMaps.Count);
            Console.WriteLine(
                $"LiveHTS Confirmatory Test:{_allConfirmatoryTestQuestions.Count}:EMR:{_confirmatoryTestMaps.Count}");
        }

        [Test]
        public void should_map_confirmatory_test_to_valid_destination()
        {
            Assert.True(_confirmatoryTestMaps.Count > 0);
            foreach (var confirmatoryTestMap in _confirmatoryTestMaps)
            {
                Assert.DoesNotThrow(() =>
                {
                    var count = _emrConnection.ExecuteScalar<int>(confirmatoryTestMap.SqlColumn());

                });
            }

            foreach (var confirmatoryTestMap in _confirmatoryTestMaps)
            {
                Console.WriteLine(confirmatoryTestMap.Info());
            }
        }

        [Test]
        public void should_have_mapped_confirmatory_test_lookups()
        {
            Assert.True(_confirmatoryTestBindMaps.Count > 0);

            foreach (var p in _confirmatoryTestBindMaps)
            {
                var subs = _hapiConnection.GetAll<SubscriberTranslation>()
                    .Where(x => x.Ref.Trim().ToLower() == p.TranslationField.Trim().ToLower() &&
                                x.Group == 4
                                ).ToList();
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
        public void should_have_valid_confirmatory_test_lookups()
        {
            Assert.True(_confirmatoryTestBindMaps.Count > 0);

            foreach (var p in _confirmatoryTestBindMaps)
            {
                Console.WriteLine($"Lookups   |   {p.Display}");
                var mappedLookups = _hapiConnection.GetAll<SubscriberTranslation>()
                    .Where(
                        x => x.Ref.Trim().ToLower() == p.TranslationField.Trim().ToLower() &&
                             x.Group == 4
                    ).ToList();
                Assert.True(mappedLookups.Count > 0);

                var lookups = _emrConnection.Query<LookupMap>(p.GetLookups()).ToList();
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