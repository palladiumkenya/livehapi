using System;
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
            foreach (var encounter in encounters)
            {
                
            }
        }
    }
}