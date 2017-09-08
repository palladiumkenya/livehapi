using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using FizzWare.NBuilder;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.ValueModel;

namespace LiveHAPI.Shared.Tests.TestHelpers
{
    public class TestData
    {
        private static readonly int _count = 2;
        private static List<County> _counties = new List<County>();
        private static List<SubCounty> _subcounties = new List<SubCounty>();
        private static List<PracticeType> _pracTypes = new List<PracticeType>();
        private static List<Practice> _pracs = new List<Practice>();
        private static List<Practice> _pracWithActivation = new List<Practice>();
        private static List<PracticeActivation> _pracActvs = new List<PracticeActivation>();
        private static List<DeviceIdentity> _devices = new List<DeviceIdentity>();
        public static void Init()
        {
            _counties = TestCounties();
            _subcounties = TestSubCounties();
            _pracTypes = TestPracticeTypes();
            _pracs = TestPractices();
            _devices = TestDevices();
            _pracActvs = TestPracticeActivations();
            _pracWithActivation = TestPracticeWithActivation();
        }

        public static List<County> TestCounties()
        {
            if (_counties.Count > 0) return _counties;

            var list = Builder<County>.CreateListOfSize(_count)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();

            list[0].Id = 41;
            list[0].Name = "Siaya";
            list[1].Id = 47;
            list[1].Name = "Nairobi";
            return list;
        }

        public static List<SubCounty> TestSubCounties()
        {
            if (_subcounties.Count > 0) return _subcounties;

            var list = Builder<SubCounty>.CreateListOfSize(4)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();


            list[0].CountyId = TestCounties()[0].Id;
            list[1].CountyId = TestCounties()[0].Id;
            list[2].CountyId = TestCounties()[1].Id;
            list[3].CountyId = TestCounties()[1].Id;
            return list;
        }

        public static List<PracticeType> TestPracticeTypes()
        {

            if (_pracTypes.Count > 0) return _pracTypes;

            var list = Builder<PracticeType>.CreateListOfSize(1)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();

            list[0].Id = "Facility";
            list[0].Name = "Facility";
            return list;
        }

        public static List<Practice> TestPractices()
        {

            if (_pracs.Count > 0) return _pracs;

            var list = Builder<Practice>.CreateListOfSize(_count)
                .All()
                .With(x => x.IsDefault = false)
                .With(x => x.Voided = false)
                .With(x => x.PracticeTypeId = TestPracticeTypes()[0].Id)
                .Build()
                .ToList();

            list[0].CountyId = TestCounties()[0].Id; 
            list[0].Code = "14080 ";
            list[0].Name = "Siaya District Hospital";

            list[1].CountyId = TestCounties()[1].Id; 
            list[1].Name = "Kenyatta National Hospital";
            list[1].Code = "13023";

            return list;
        }
        public static List<Practice> TestPracticeWithActivation()
        {

            if (_pracWithActivation.Count > 0) return _pracWithActivation;

            var list = TestPractices();
            list[0].Activations.Add(TestPracticeActivations().First(x=>x.PracticeId == list[0].Id));
            list[1].Activations.Add(TestPracticeActivations().First(x => x.PracticeId == list[1].Id));
            return list;
        }
        public static List<DeviceIdentity> TestDevices()
        {
            if (_devices.Count > 0) return _devices;

            var list = new List<DeviceIdentity>
            {
                new DeviceIdentity("S1", "HTC 10", "1001"),
                new DeviceIdentity("S2", "SAMSUNG S8", "2002")
            };
            return list;
        }

        public static List<PracticeActivation> TestPracticeActivations()
        {
            if (_pracActvs.Count > 0) return _pracActvs;

            var list = Builder<PracticeActivation>.CreateListOfSize(_count)
                .All()
                .With(x => x.Voided = false)
                .With(x => x.ActivationDate = DateTime.Now.AddYears(-1))
                .With(x => x.ActivationCode = "XXX")
                .With(x => x.ExpiryDate = DateTime.Now.AddDays(-2))
                .Build()
                .ToList();

            list[0].Device = TestDevices()[0].Serial;
            list[0].Model = TestDevices()[0].Model;
            list[0].DeviceCode = TestDevices()[0].Code;
            list[0].PracticeId = TestPractices()[0].Id;

            list[1].Device = TestDevices()[1].Serial;
            list[1].Model = TestDevices()[1].Model;
            list[1].DeviceCode = TestDevices()[1].Code;
            list[1].PracticeId = TestPractices()[1].Id;

            return list;
        }
    }
}