using System;
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
    public class PracticeTests
    {
        private List<Practice> _practiceWithActivations;
        private List<DeviceInfo> _devices;

        
        [SetUp]
        public void SetUp()
        {
            TestData.Init();
            _devices = TestData.TestDevices();
            _practiceWithActivations = TestData.TestPracticeWithActivation();            
        }

        [Test]
        public void should_Activate_Device_New()
        {
            var practice = _practiceWithActivations.First();

            var device = _devices.First();
            device.Serial = "X3";

            var activation = practice.ActivateDevice(device);
            Assert.IsTrue(activation.IsActive());
            Assert.IsFalse(activation.IsExpired());
            Console.WriteLine(activation);

        }

        [Test]
        public void should_Activate_Device_Renew()
        {
            var practice = _practiceWithActivations.Last();
            var activationExpired = practice.Activations.First(x => x.IsExpired());
            var device = _devices.First(x => x.Serial == activationExpired.Device);

            var activation = practice.ActivateDevice(device);
            Assert.IsTrue(activation.IsActive());
            Assert.IsFalse(activation.IsExpired());
            Console.WriteLine(activation);

        }
    }
} 