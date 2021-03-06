﻿using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Shared.Tests.TestHelpers;
using LiveHAPI.Shared.ValueObject;
using NUnit.Framework;

namespace LiveHAPI.Core.Tests.Model.Network
{
    [TestFixture]
    public class PracticeActivationTests
    {
        private List<Practice> _practiceWithActivations;
        private List<DeviceInfo> _devices;
        private Guid _practiceId;


        [SetUp]
        public void SetUp()
        {
            TestData.Init();
            _devices = TestData.TestDevices();
            _practiceWithActivations = TestData.TestPracticeWithActivation();
            _practiceId = _practiceWithActivations.First().Id;
        }

        [Test]

        public void should_Create_NewActivation()
        {
            var device = _devices.First();
            device.Serial = "X3";

            var activation = PracticeActivation.Create(_practiceId, device);

            Assert.IsTrue(activation.IsActive());
            Assert.IsFalse(activation.IsExpired());
            Console.WriteLine(activation);
        }
        [Test]
        public void should_Create_New_No_Activation()
        {
            var device = _devices.First();
            device.Serial = "X3";

            var activation = PracticeActivation.Create(_practiceId,device, null,false);

            Assert.IsFalse(activation.IsActive());
            Console.WriteLine(activation);
        }

        [TestCase(1,"A001")]
        [TestCase(101, "A101")]
        [TestCase(1011, "A1011")]
        [TestCase(10111, "A10111")]
        public void should_Generte_IdentifierPrefix(int id,string idPrefix)
        {
            var device = _devices.First();
            device.Serial = "X3";

            var activation = PracticeActivation.Create(_practiceId, device, null, false);

            activation.Identifier = id;
          
            Assert.AreEqual(idPrefix,activation.IdentifierPrefix);
            Console.WriteLine($"{id} > {activation.IdentifierPrefix}");
        }
    }
} 