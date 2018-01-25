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
    public class PartnerScreeningMapTests
    {
        private SqlConnection _hapiConnection;
        private SqlConnection _emrConnection;

        private readonly string _sqlPartnerScreeningMap = PartnerScreeningMap.GetQuery();
        private readonly string _sqlPartnerScreeningBindMap = PartnerScreeningBindMap.GetQuery();

        private List<SubscriberMap> _allPartnerScreeningQuestions;
        private List<PartnerScreeningMap> _partnerScreeningMaps;
        private List<PartnerScreeningBindMap> _partnerScreeningBindMaps;
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

            _allPartnerScreeningQuestions = _hapiConnection.GetAll<SubscriberMap>().Where(
                x => x.Name.ToLower().Trim() == "ObsPartnerScreening".ToLower().Trim()).ToList();

            _partnerScreeningMaps = _hapiConnection.Query<PartnerScreeningMap>(_sqlPartnerScreeningMap).ToList();
            _partnerScreeningBindMaps = _hapiConnection.Query<PartnerScreeningBindMap>(_sqlPartnerScreeningBindMap).ToList();
        }

        [SetUp]
        public void SetUp()
        {
            _hapiConnection = new SqlConnection(_config["connectionStrings:hAPIConnection"]);
            _emrConnection = new SqlConnection(_config["connectionStrings:EMRConnection"]);
        }

        [Test]
        public void should_map_partnerScreening_non_alien()
        {
            Assert.True(_allPartnerScreeningQuestions.Count > 0);
            Assert.True(_partnerScreeningMaps.Count > 0);
            Assert.AreEqual(_allPartnerScreeningQuestions.Count, _partnerScreeningMaps.Count);
            Console.WriteLine(
                $"LiveHTS PartnerScreening:{_allPartnerScreeningQuestions.Count}:EMR:{_partnerScreeningMaps.Count}");
        }

        [Test]
        public void should_map_partnerScreening_to_valid_destination()
        {
            Assert.True(_partnerScreeningMaps.Count > 0);

            foreach (var partnerScreeningMap in _partnerScreeningMaps)
            {
                Assert.DoesNotThrow(() =>
                {
                    var count = _emrConnection.ExecuteScalar<int>(partnerScreeningMap.SqlColumn());

                });
            }

            foreach (var partnerScreeningMap in _partnerScreeningMaps)
            {
                Console.WriteLine(partnerScreeningMap.Info());
            }
        }

        [Test]
        public void should_have_mapped_partnerScreening_lookups()
        {
            Assert.True(_partnerScreeningBindMaps.Count > 0);

            foreach (var p in _partnerScreeningBindMaps)
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
        public void should_have_valid_partnerScreening_lookups()
        {
            Assert.True(_partnerScreeningBindMaps.Count > 0);

            foreach (var p in _partnerScreeningBindMaps)
            {
                Console.WriteLine($"Lookups   |   {p.Display}");
                var mappedLookups = _hapiConnection.GetAll<SubscriberTranslation>()
                    .Where(
                        x => x.Ref.Trim().ToLower() == p.TranslationField.Trim().ToLower()).ToList();
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