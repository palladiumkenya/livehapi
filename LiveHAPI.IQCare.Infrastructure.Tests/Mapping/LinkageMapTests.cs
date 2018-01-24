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
    public class LinkageMapTests
    {
        private SqlConnection _hapiConnection;
        private SqlConnection _emrConnection;

        private readonly string _sqlLinkageMap = LinkageMap.GetQuery();
        private readonly string _sqlLinkageBindMap = LinkageBindMap.GetQuery();

        private List<SubscriberMap> _allLinkageQuestions;
        private List<LinkageMap> _linkageMaps;
        private List<LinkageBindMap> _linkageBindMaps;
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

            _allLinkageQuestions = _hapiConnection.GetAll<SubscriberMap>().Where(
                x => x.Name.ToLower().Trim() == "ObsLinkage".ToLower().Trim()).ToList();

            _linkageMaps = _hapiConnection.Query<LinkageMap>(_sqlLinkageMap).ToList();
            _linkageBindMaps = _hapiConnection.Query<LinkageBindMap>(_sqlLinkageBindMap).ToList();
        }

        [SetUp]
        public void SetUp()
        {
            _hapiConnection = new SqlConnection(_config["connectionStrings:hAPIConnection"]);
            _emrConnection = new SqlConnection(_config["connectionStrings:EMRConnection"]);
        }

        [Test]
        public void should_map_linkage_non_alien()
        {
            Assert.True(_allLinkageQuestions.Count > 0);
            Assert.True(_linkageMaps.Count > 0);
            Assert.AreEqual(_allLinkageQuestions.Count, _linkageMaps.Count);
            Console.WriteLine(
                $"LiveHTS Linkage:{_allLinkageQuestions.Count}:EMR:{_linkageMaps.Count}");
        }

        [Test]
        public void should_map_linkage_to_valid_destination()
        {
            Assert.True(_linkageMaps.Count > 0);

            foreach (var linkageMap in _linkageMaps)
            {
                Assert.DoesNotThrow(() =>
                {
                    var count = _emrConnection.ExecuteScalar<int>(linkageMap.SqlColumn());

                });
            }

            foreach (var linkageMap in _linkageMaps)
            {
                Console.WriteLine(linkageMap.Info());
            }
        }

        [Test]
        public void should_have_not_mapped_linkage_lookups()
        {
            Assert.False(_linkageBindMaps.Count > 0);
            /*
            foreach (var p in _linkageBindMaps)
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
            */
        }

        [Test]
        public void should_not_have_valid_linkage_lookups()
        {
            Assert.False(_linkageBindMaps.Count > 0);
            /*
            foreach (var p in _linkageBindMaps)
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
            */
        }

        [TearDown]
        public void TearDown()
        {
            _emrConnection.Dispose();
            _hapiConnection.Dispose();
        }
    }
}