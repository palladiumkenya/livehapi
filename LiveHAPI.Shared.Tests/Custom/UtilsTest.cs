using System;
using System.Linq;
using FizzWare.NBuilder;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Shared.Custom;
using NUnit.Framework;

namespace LiveHAPI.Shared.Tests.Custom
{
    [TestFixture]
    public class UtilsTest
    {
        [Test]
        public void should_decrypt_passwod()
        {
            string password = "Zu7BrcApEvdWiVLpjGpuhw==";
            var decryptedPassword = Utils.Decrypt(password);
            Assert.AreEqual("c0nste11a",decryptedPassword);
            Console.WriteLine($"Password [{password}] >> [{decryptedPassword}]");
        }

        [Test]
        public void should_group_by()
        {
            var cid = LiveGuid.NewGuid();
            var encounters = Builder<Encounter>.CreateListOfSize(2).All().With(x=>x.ClientId=cid) .Build();
            foreach (var encounter in encounters)
            {
                var obses = Builder<Obs>.CreateListOfSize(3).All().With(x => x.EncounterId =encounter.Id).Build();
                foreach (var obse in obses)
                {
                    encounter.Obses.Add(obse);
                }
                
            }

            var allObs = encounters.SelectMany(x => x.Obses).ToList();
            Assert.True(allObs.Count>0);
            var obsGroups = allObs
                .GroupBy(x => x.EncounterId).ToList();

            Assert.True(obsGroups.Count == 2);
            foreach (var obsGroup in obsGroups)
            {
                Console.WriteLine($"{obsGroup.ToList().First().EncounterId}");
                foreach (var o in obsGroup.ToList())
                {
                    Console.WriteLine($"        {o.Id}");   
                }
            }
                
            

            foreach (var obsGroup in obsGroups)
            {

                // obsGroup.
            }

            foreach (var obs in allObs)
            {
                Console.WriteLine($"{obs.Id} | {obs.EncounterId} | {obs.ValueText}");
            }
        }
        
        [Test]
        public void should_Determin_Null_Date_if_min()
        {
            DateTime? dateTime = DateTime.MinValue;
            Assert.IsTrue(dateTime.IsNullOrEmpty());
            Console.WriteLine(dateTime.ToString());

        }



        [Test]
        public void should_convert_NullDate_To_IQDate()
        {
            DateTime? dateTime = DateTime.MinValue;
            Assert.IsTrue(string.IsNullOrEmpty(dateTime.ToIqDate()));
            Console.WriteLine($"{dateTime:F} >> {dateTime.ToIqDate()}");

            dateTime = null;
            Assert.IsTrue(string.IsNullOrEmpty(dateTime.ToIqDate()));
            Console.WriteLine($"{dateTime:F} >> {dateTime.ToIqDate()}");
        }

        [Test]
        public void should_convert_NullDate_ToIqDateOnly()
        {

            DateTime? dateTime = DateTime.MinValue;
            Assert.IsTrue(string.IsNullOrEmpty(dateTime.ToIqDateOnly()));
            Console.WriteLine($"{dateTime:d} >> {dateTime.ToIqDateOnly()}");

            dateTime = null;
            Assert.IsTrue(string.IsNullOrEmpty(dateTime.ToIqDateOnly()));
            Console.WriteLine($"{dateTime:F} >> {dateTime.ToIqDateOnly()}");


        }

        [Test]
        public void should_convert_ToIqDate()
        {
            DateTime dateTime = DateTime.Today.AddYears(3);
            Assert.False(string.IsNullOrEmpty(dateTime.ToIqDate()));
            Console.WriteLine($"{dateTime:F} >> {dateTime.ToIqDate()}");

            DateTime dateTime2 = DateTime.MinValue;
            Assert.IsTrue(string.IsNullOrEmpty(dateTime2.ToIqDate()));
            Console.WriteLine($"{dateTime2:F} >> {dateTime2.ToIqDate()}");
        }

        [Test]
        public void should_convert_ToIqDateOnly()
        {
            DateTime dateTime = DateTime.Today.AddYears(3);
            Assert.False(string.IsNullOrEmpty(dateTime.ToIqDateOnly()));
            Console.WriteLine($"{dateTime:d} >> {dateTime.ToIqDateOnly()}");

            DateTime dateTime2 = DateTime.MinValue;
            Assert.IsTrue(string.IsNullOrEmpty(dateTime2.ToIqDateOnly()));
            Console.WriteLine($"{dateTime2:d} >> {dateTime2.ToIqDateOnly()}");
        }

    }
}