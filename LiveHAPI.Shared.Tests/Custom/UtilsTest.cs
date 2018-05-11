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
    }
}