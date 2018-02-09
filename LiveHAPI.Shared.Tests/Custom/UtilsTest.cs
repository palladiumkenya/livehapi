using System;
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
    }
}