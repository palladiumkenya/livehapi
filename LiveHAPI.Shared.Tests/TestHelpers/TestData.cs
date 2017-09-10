using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using FizzWare.NBuilder;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Shared.Tests.TestHelpers
{
    public class TestData
    {
        private static readonly int _count = 2;
        private static List<County> _counties = new List<County>();
        private static List<MasterFacility> _facilities=new List<MasterFacility>();
        private static List<SubCounty> _subcounties = new List<SubCounty>();
        private static List<PracticeType> _pracTypes = new List<PracticeType>();
        private static List<Practice> _pracs = new List<Practice>();
        private static List<Practice> _pracWithActivation = new List<Practice>();
        private static List<Person> _persons = new List<Person>();
        private static List<User> _users = new List<User>();

        private static List<PracticeActivation> _pracActvs = new List<PracticeActivation>();
        private static List<DeviceInfo> _devices = new List<DeviceInfo>();
        private static List<PersonInfo> _personInfos = new List<PersonInfo>();
        private static List<UserInfo> _userInfos = new List<UserInfo>();

        public static void Init()
        {
            _counties = TestCounties();
            _facilities = TestFacilities();
            _subcounties = TestSubCounties();
            _pracTypes = TestPracticeTypes();
            _pracs = TestPractices();
            _persons = TestPersons();
            
            _devices = TestDevices();
            _pracActvs = TestPracticeActivations();
            _pracWithActivation = TestPracticeWithActivation();
            _users = TestUsers();
            _personInfos = TestPersonInfos();
            _userInfos = TestUserInfos();
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
        public static List<MasterFacility> TestFacilities()
        {
            if (_facilities.Count > 0) return _facilities;

            var list = new List<MasterFacility>
            {
                new MasterFacility(13023,"Kenyatta National Hospital",47,"Nairobi"),
                new MasterFacility(13080,"Mbagathi District Hospital",47,"Nairobi"),
                new MasterFacility(14080,"Siaya District Hospital",41,"Siaya"),
                new MasterFacility(16792,"Wagai Dispensary",41,"Siaya"),
            };
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
            list[0].AddActivation(TestPracticeActivations()[0]);
            list[1].AddActivation(TestPracticeActivations()[1]);
            return list;
        }

        public static List<Person> TestPersons()
        {
            if (_persons.Count > 0) return _persons;

            var personNames = Builder<PersonName>.CreateListOfSize(_count).Build().ToList();
            personNames[0].Source = "14080";
            personNames[0].SourceRef = "1";
            personNames[0].SourceRef = "KenyaEMR";

            personNames[1].Source = "13023";
            personNames[1].SourceRef = "1";
            personNames[1].SourceSys = "IQCare";

            var list = Builder<Person>.CreateListOfSize(_count)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();
            
            list[0].AssignName(personNames[0]);
            list[1].AssignName(personNames[1]);

            return list;
        }

        public static List<User> TestUsers()
        {
            if (_users.Count > 0) return _users;

            var list = Builder<User>.CreateListOfSize(_count)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();

            list[0].PersonId = TestPersons()[0].Id;
            list[0].PracticeId = TestPracticeWithActivation()[0].Id;
            list[0].Source = "14080";
            list[0].SourceRef = "10";
            list[0].SourceRef = "KenyaEMR";

             
            list[1].PersonId = TestPersons()[1].Id;
            list[1].PracticeId = TestPracticeWithActivation()[1].Id;
            list[1].Source = "13023";
            list[1].SourceRef = "10";
            list[1].SourceSys = "IQCare";

            return list;
        }

        public static List<DeviceInfo> TestDevices()
        {
            if (_devices.Count > 0) return _devices;

            var list = new List<DeviceInfo>
            {
                new DeviceInfo("S1", "HTC 10", "1001"),
                new DeviceInfo("S2", "SAMSUNG S8", "2002")
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
        private static List<PersonInfo> TestPersonInfos()
        {
            if (_personInfos.Count > 0) return _personInfos;

            var personInfos = Builder<PersonInfo>.CreateListOfSize(4).Build().ToList();
            var identities = Builder<Identity>.CreateListOfSize(4).Build().ToList();

            var p1 = personInfos[0];
            p1.Identity = identities[0];
            p1.Identity.Source = "14080";
            p1.Identity.SourceRef = "1";
            p1.Identity.SourceRef = "KenyaEMR";

            var p3 = personInfos[1];
            p3.Identity = identities[1];
            p3.Identity.Source = "14080";
            p3.Identity.SourceRef = "2";
            p3.Identity.SourceRef = "KenyaEMR";


            var p2 = personInfos[2];
            p2.Identity = identities[2];
            p2.Identity.Source = "13023";
            p2.Identity.SourceRef = "1";
            p2.Identity.SourceSys = "IQCare";

            var p4 = personInfos[3];
            p4.Identity = identities[3];
            p4.Identity.Source = "13023";
            p4.Identity.SourceRef = "2";
            p4.Identity.SourceSys = "IQCare";

            return new List<PersonInfo> {p1, p2, p3, p4};
        }

        private static List<UserInfo> TestUserInfos()
        {
            if (_userInfos.Count > 0) return _userInfos;


            var userInfos = Builder<UserInfo>.CreateListOfSize(4).Build().ToList();
            var identities = Builder<Identity>.CreateListOfSize(4).Build().ToList();

            var u1 = userInfos[0];
            u1.Identity = identities[0];
            u1.Identity.Source = "14080";
            u1.Identity.SourceRef = "10";
            u1.Identity.SourceSys = "KenyaEMR";
            u1.PersonInfo = TestPersonInfos()[0];

            var u2 = userInfos[1];
            u1.Identity = identities[1];
            u1.Identity.Source = "14080";
            u1.Identity.SourceRef = "11";
            u1.Identity.SourceSys = "KenyaEMR";
            u1.PersonInfo = TestPersonInfos()[1];

            var u3 = userInfos[2];
            u3.Identity = identities[2];
            u3.Identity.Source = "13023";
            u3.Identity.SourceRef = "10";
            u3.Identity.SourceSys = "IQCare";
            u3.PersonInfo = TestPersonInfos()[2];

            var u4 = userInfos[3];
            u4.Identity = identities[3];
            u4.Identity.Source = "13023";
            u4.Identity.SourceRef = "11";
            u4.Identity.SourceSys = "IQCare";
            u4.PersonInfo = TestPersonInfos()[3];

            return new List<UserInfo> {u1, u2, u3, u4};
        }
    }
}