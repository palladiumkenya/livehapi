using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using LiveHAPI.Core.Model.QModel;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LiveHAPI.IQCare.Infrastructure.Tests.Mapping
{
    [TestFixture]
    public class PreTestMapTests
    {
        private SqlConnection _hapiConnection;
        private SqlConnection _emrConnection;

        private readonly string _sqlPretestMap = $@"
            SELECT        Questions.Id, SubscriberMaps.Field, Questions.Display, SubscriberMaps.Name, SubscriberMaps.SubName, SubscriberMaps.SubField, SubscriberMaps.Mode
            FROM            Questions INNER JOIN
                                     SubscriberMaps ON CAST(Questions.Id AS varchar(50)) = SubscriberMaps.Field
            WHERE        (Questions.Fact <> N'alien') ";

        private List<Question> _allQuestions;
        private List<PreTestMap> _preTestMaps;

        [SetUp]
        public void SetUp()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _hapiConnection = new SqlConnection(config["connectionStrings:hAPIConnection"]);
            _emrConnection = new SqlConnection(config["connectionStrings:EMRConnection"]);
            _allQuestions = _hapiConnection.GetAll<Question>().Where(x => !x.Fact.Equals("alien")).ToList();
            _preTestMaps = _hapiConnection.Query<PreTestMap>(_sqlPretestMap).ToList();
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

        [TearDown]
        public void should_Get_VisitTypes()
        {
           _emrConnection.Dispose();
            _hapiConnection.Dispose();
        }
    }
}