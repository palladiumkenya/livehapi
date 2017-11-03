using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveHAPI.Shared.Custom;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LiveHAPI.Shared.Tests
{
    [TestClass]
    public class SearchHitTests
    {
        private List<SearchHit> _searchHits;
        private Guid _id1;
        private Guid _id2;

        [TestInitialize]
        public void SetUp()
        {
            _id1 = LiveGuid.NewGuid();
            _id2 = LiveGuid.NewGuid();
            _searchHits = new List<SearchHit> {new SearchHit(_id1), new SearchHit(_id1) ,new SearchHit(_id2) };
        }

        [TestMethod]
        public void should_Rank_by_hits()
        {
            var rankedHits= _searchHits.GroupBy(d => d.ItemId)
                .Select(
                    g => new
                    {
                        Key =g.First().ItemId,
                        Value = g.Sum(s => s.Hits)
                    })
                    .ToList();
            var no1 = rankedHits.FirstOrDefault(x => x.Value == 2);
            Assert.AreEqual(_id1,no1.Key);
            Assert.IsTrue(rankedHits.Count==2);
           
            foreach (var rankedHit in rankedHits)
            {
                Console.WriteLine($"{rankedHit.Key}|{rankedHit.Value}");
            }
        }
    }
}
