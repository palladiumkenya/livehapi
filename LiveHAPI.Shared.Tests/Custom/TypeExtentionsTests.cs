using System;
using System.Collections.Generic;
using System.Text;
using LiveHAPI.Shared.Custom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiveHAPI.Shared.Tests.Custom
{
    [TestClass]
    public class TypeExtentionsTests
    {

        [TestMethod]
        public void should_Sanitize_String()
        {
            var name = @"ng'ang'a";

            var sanitizedName = name.Sanitize();
            Assert.AreEqual(@"ng''ang''a", sanitizedName);

            Console.WriteLine($"{name} > {sanitizedName}");

        }

        [TestMethod]
        public void should_Sanitize_Null_String()
        {
            string name = null;

            var sanitizedName = name.Sanitize();
            Assert.AreEqual(string.Empty, sanitizedName);

            

        }
    }
}
