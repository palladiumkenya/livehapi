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
    public class MemberScreeningMapTests
    {
        private SqlConnection _hapiConnection;
        private SqlConnection _emrConnection;

        private readonly string _sqlMemberScreeningMap = MemberScreeningMap.GetQuery();
        private readonly string _sqlMemberScreeningBindMap = MemberScreeningBindMap.GetQuery();

        private List<SubscriberMap> _allMemberScreeningQuestions;
        private List<MemberScreeningMap> _memberScreeningMaps;
        private List<MemberScreeningBindMap> _memberScreeningBindMaps;
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

            _allMemberScreeningQuestions = _hapiConnection.GetAll<SubscriberMap>().Where(
                x => x.Name.ToLower().Trim() == "ObsMemberScreening".ToLower().Trim()).ToList();

            _memberScreeningMaps = _hapiConnection.Query<MemberScreeningMap>(_sqlMemberScreeningMap).ToList();
            _memberScreeningBindMaps = _hapiConnection.Query<MemberScreeningBindMap>(_sqlMemberScreeningBindMap).ToList();
        }

        [SetUp]
        public void SetUp()
        {
            _hapiConnection = new SqlConnection(_config["connectionStrings:hAPIConnection"]);
            _emrConnection = new SqlConnection(_config["connectionStrings:EMRConnection"]);
        }

        [Test]
        public void should_map_memberScreening_non_alien()
        {
            Assert.True(_allMemberScreeningQuestions.Count > 0);
            Assert.True(_memberScreeningMaps.Count > 0);
            Assert.AreEqual(_allMemberScreeningQuestions.Count, _memberScreeningMaps.Count);
            Console.WriteLine(
                $"LiveHTS MemberScreening:{_allMemberScreeningQuestions.Count}:EMR:{_memberScreeningMaps.Count}");
        }

        [Test]
        public void should_map_memberScreening_to_valid_destination()
        {
            Assert.True(_memberScreeningMaps.Count > 0);

            foreach (var memberScreeningMap in _memberScreeningMaps)
            {
                Assert.DoesNotThrow(() =>
                {
                    var count = _emrConnection.ExecuteScalar<int>(memberScreeningMap.SqlColumn());

                });
            }

            foreach (var memberScreeningMap in _memberScreeningMaps)
            {
                Console.WriteLine(memberScreeningMap.Info());
            }
        }

        [Test]
        public void should_have_mapped_memberScreening_lookups()
        {
            Assert.True(_memberScreeningBindMaps.Count > 0);

            foreach (var p in _memberScreeningBindMaps)
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
        public void should_have_valid_memberScreening_lookups()
        {
            Assert.True(_memberScreeningBindMaps.Count > 0);

            foreach (var p in _memberScreeningBindMaps)
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