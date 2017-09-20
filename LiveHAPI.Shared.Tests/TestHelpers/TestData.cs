using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Json;
using FizzWare.NBuilder;
using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Studio;
using LiveHAPI.Shared.ValueObject;
using Newtonsoft.Json;

namespace LiveHAPI.Shared.Tests.TestHelpers
{
    public class  TestData
    {
        private static readonly int _count = 2;
        private static List<County> _counties = new List<County>();
        private static List<MasterFacility> _facilities=new List<MasterFacility>();
        private static List<SubCounty> _subcounties = new List<SubCounty>();
        private static List<PracticeType> _pracTypes = new List<PracticeType>();
        private static List<ProviderType> _providerTypes = new List<ProviderType>();
        private static List<IdentifierType> _identifierTypes = new List<IdentifierType>();
        private static List<RelationshipType> _relationshipTypes = new List<RelationshipType>();
        private static List<ConceptType> _conceptTypes = new List<ConceptType>();
        private static List<Practice> _pracs = new List<Practice>();
        private static List<Practice> _pracWithActivation = new List<Practice>();
        private static List<Person> _persons = new List<Person>();
        private static List<User> _users = new List<User>();
        private static List<Provider> _providers=new List<Provider>();
        private static List<Client> _clients = new List<Client>();
        private static List<Encounter> _encounters = new List<Encounter>();


        private static List<Module> _modules = new List<Module>();
        private static List<Form> _forms = new List<Form>();
        private static List<EncounterType> _encounterTypes = new List<EncounterType>();
        private static List<Concept> _concepts = new List<Concept>();
        private static List<Question> _questions = new List<Question>();

        private static List<PracticeActivation> _pracActvs = new List<PracticeActivation>();
        private static List<DeviceInfo> _devices = new List<DeviceInfo>();
        private static List<PersonNameInfo> _personNameInfos = new List<PersonNameInfo>();
        private static List<UserInfo> _userInfos = new List<UserInfo>();
        private static List<ProviderInfo> _providerInfos = new List<ProviderInfo>();

        private static List<ClientInfo> _clientInfos = new List<ClientInfo>();

        private static List<IdentifierInfo> _identifierInfos = new List<IdentifierInfo>();
        private static List<RelationshipInfo> _relationshipInfos = new List<RelationshipInfo>();

        private static List<PersonInfo> _personInfos = new List<PersonInfo>();

        private static List<AddressInfo> _addressInfos = new List<AddressInfo>();
        private static List<ContactInfo> _contactInfos = new List<ContactInfo>();
        private static List<EncounterInfo> _encounterInfos =new List<EncounterInfo>();
        private static List<ObsInfo> _obsInfos=new List<ObsInfo>();

        


        public static void Init()
        {

             _counties = new List<County>();
             _facilities = new List<MasterFacility>();
             _subcounties = new List<SubCounty>();
             _pracTypes = new List<PracticeType>();
             _providerTypes = new List<ProviderType>();
             _identifierTypes = new List<IdentifierType>();
            _relationshipTypes = new List<RelationshipType>();
             _pracs = new List<Practice>();
             _pracWithActivation = new List<Practice>();
            _persons = new List<Person>();
             _users = new List<User>();
             _providers = new List<Provider>();
             _clients = new List<Client>();



             _pracActvs = new List<PracticeActivation>();
             _devices = new List<DeviceInfo>();
             _personNameInfos = new List<PersonNameInfo>();
             _userInfos = new List<UserInfo>();
             _providerInfos = new List<ProviderInfo>();

             _clientInfos = new List<ClientInfo>();

             _identifierInfos = new List<IdentifierInfo>();
             _relationshipInfos = new List<RelationshipInfo>();

             _personInfos = new List<PersonInfo>();

             _addressInfos = new List<AddressInfo>();
             _contactInfos = new List<ContactInfo>();

            _conceptTypes=new List<ConceptType>();
            _modules=new List<Module>();
            _forms = new List<Form>();
            _encounterTypes = new List<EncounterType>();
            _concepts=new List<Concept>();
            _questions=new List<Question>();

            _encounters=new List<Encounter>();

            _encounterInfos=new List<EncounterInfo>();
            _obsInfos=new List<ObsInfo>();


        _counties = TestCounties();
            _facilities = TestFacilities();
            _subcounties = TestSubCounties();
            _pracTypes = TestPracticeTypes();
            _providerTypes = TestProviderTypes();
            _identifierTypes = TestIdentifierTypes();
            _relationshipTypes = TestRelationshipTypes();

            _pracs = TestPractices();
            _devices = TestDevices();
            _pracActvs = TestPracticeActivations();
            _pracWithActivation = TestPracticeWithActivation();
            _persons = TestPersons();
            _users = TestUsers();
            _clients = TestClients();

            _providers = TestProviders();

            _personNameInfos = TestPersonNameInfos();
            _userInfos = TestUserInfos();
            _providerInfos = TestProviderInfos();

            _encounterTypes = TestEncounterTypes();
            _conceptTypes = TestConceptTypes();
            _concepts = TestConcepts();
            _modules = TestModules();
            _forms = TestForms();
            _questions = TestQuestions();

            _encounters = TestEncounters();
            _personInfos = TestPersonInfos();
            _addressInfos = TestAddressInfos();
            _contactInfos = TestContactInfos();
            _relationshipInfos = TestRelationshipInfos();
            _clientInfos = TestClientInfos();
            _identifierInfos = TestIdentifierInfos();

            _encounterInfos = TestEncounterInfos();
            _obsInfos = TestObsInfos();

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
        public static List<IdentifierType> TestIdentifierTypes()
        {
            if (_identifierTypes.Count > 0) return _identifierTypes;

            var list = Builder<IdentifierType>.CreateListOfSize(1)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();

            list[0].Id = "Serial";
            list[0].Name = "Serial";
            return list;
        }
        public static List<RelationshipType> TestRelationshipTypes()
        {
            if (_relationshipTypes.Count > 0) return _relationshipTypes;

            var list = Builder<RelationshipType>.CreateListOfSize(1)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();

            list[0].Id = "Partner";
            list[0].Name = "Partner";
            return list;
        }
        public static List<ProviderType> TestProviderTypes()
        {
            if (_providerTypes.Count > 0) return _providerTypes;

            var list = Builder<ProviderType>.CreateListOfSize(1)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();

            list[0].Id = "HW";
            list[0].Name = "Health Worker";
            return list;
        }
        public static List<ConceptType> TestConceptTypes()
        {
            if (_conceptTypes.Count > 0) return _conceptTypes;

            var list = Builder<ConceptType>.CreateListOfSize(1)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();

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

            var personNames = Builder<PersonName>.CreateListOfSize(4).Build().ToList();
            personNames[0].Source = "14080";
            personNames[0].SourceRef = "1";
            personNames[0].SourceSys = "KenyaEMR";

            personNames[1].Source = "13023";
            personNames[1].SourceRef = "1";
            personNames[1].SourceSys = "IQCare";

            personNames[2].Source = "14080";
            personNames[2].SourceRef = "2";
            personNames[2].SourceSys = "KenyaEMR";
           
            personNames[3].Source = "13023";
            personNames[3].SourceRef = "2";
            personNames[3].SourceSys = "IQCare";

            var personAddresses = Builder<PersonAddress>.CreateListOfSize(2).All().With(x=>x.CountyId=TestCounties()[0].Id).Build().ToList();
            var personContacts = Builder<PersonContact>.CreateListOfSize(2).Build().ToList();

            var list = Builder<Person>.CreateListOfSize(4)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();
            
            list[0].AssignName(personNames[0]);
            list[1].AssignName(personNames[1]);

            list[2].AssignName(personNames[2]);
            list[2].AssignAddress(personAddresses[0]);
            list[2].AssignContact(personContacts[0]);

            list[3].AssignName(personNames[3]);
            list[3].AssignAddress(personAddresses[1]);
            list[3].AssignContact(personContacts[1]);

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
            list[0].SourceSys = "KenyaEMR";

             
            list[1].PersonId = TestPersons()[1].Id;
            list[1].PracticeId = TestPracticeWithActivation()[1].Id;
            list[1].Source = "13023";
            list[1].SourceRef = "10";
            list[1].SourceSys = "IQCare";

            return list;
        }
        public static List<Provider> TestProviders()
        {
            if (_providers.Count > 0) return _providers;

            var list = Builder<Provider>.CreateListOfSize(_count)
                .All()
                .With(x => x.ProviderTypeId = TestProviderTypes()[0].Id)
                .With(x => x.Voided = false)
                .Build()
                .ToList();

            list[0].PersonId = TestPersons()[0].Id;
            list[0].PracticeId = TestPracticeWithActivation()[0].Id;
            list[0].Source = "14080";
            list[0].SourceRef = "20";
            list[0].SourceSys = "KenyaEMR";


            list[1].PersonId = TestPersons()[1].Id;
            list[1].PracticeId = TestPracticeWithActivation()[1].Id;
            list[1].Source = "13023";
            list[1].SourceRef = "20";
            list[1].SourceSys = "IQCare";

            return list;
            
        }
        public static List<Client> TestClients()
        {
            if (_clients.Count > 0) return _clients;

            var identifiers = Builder<ClientIdentifier>.CreateListOfSize(2).All()
                .With(x => x.IdentifierTypeId = TestIdentifierTypes()[0].Id)
                .Build().ToList();

   

            var list = Builder<Client>.CreateListOfSize(_count)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();

            var relationships = Builder<ClientRelationship>.CreateListOfSize(1)
                .All()
                .With(x => x.RelatedClientId = list[0].Id)
                .With(x => x.RelationshipTypeId = TestRelationshipTypes()[0].Id)
                .Build().ToList();

            list[0].PersonId = TestPersons()[2].Id;
            list[0].PracticeId = TestPracticeWithActivation()[0].Id;
            list[0].AddIdentifier(identifiers[0]);
            

            list[1].PersonId = TestPersons()[3].Id;
            list[1].PracticeId = TestPracticeWithActivation()[1].Id;
            list[1].AddIdentifier(identifiers[1]);
            list[1].AddRelationship(relationships[0]);

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
        
        public static List<PersonNameInfo> TestPersonNameInfos()
        {
            if (_personNameInfos.Count > 0) return _personNameInfos;

            var personInfos = Builder<PersonNameInfo>.CreateListOfSize(4).Build().ToList();
            var identities = Builder<SourceIdentity>.CreateListOfSize(4).Build().ToList();

            var p1 = personInfos[0];
            p1.SourceIdentity = identities[0];
            p1.SourceIdentity.Source = "14080";
            p1.SourceIdentity.SourceRef = "1";
            p1.SourceIdentity.SourceSys = "KenyaEMR";

            var p3 = personInfos[1];
            p3.SourceIdentity = identities[1];
            p3.SourceIdentity.Source = "14080";
            p3.SourceIdentity.SourceRef = "2";
            p3.SourceIdentity.SourceSys = "KenyaEMR";


            var p2 = personInfos[2];
            p2.SourceIdentity = identities[2];
            p2.SourceIdentity.Source = "13023";
            p2.SourceIdentity.SourceRef = "1";
            p2.SourceIdentity.SourceSys = "IQCare";

            var p4 = personInfos[3];
            p4.SourceIdentity = identities[3];
            p4.SourceIdentity.Source = "13023";
            p4.SourceIdentity.SourceRef = "2";
            p4.SourceIdentity.SourceSys = "IQCare";

            return new List<PersonNameInfo> {p1, p2, p3, p4};
        }
        public static List<UserInfo> TestUserInfos()
        {
            if (_userInfos.Count > 0) return _userInfos;


            var userInfos = Builder<UserInfo>.CreateListOfSize(4).Build().ToList();
            var identities = Builder<SourceIdentity>.CreateListOfSize(4).Build().ToList();

            var u1 = userInfos[0];
            u1.SourceIdentity = identities[0];
            u1.SourceIdentity.Source = "14080";
            u1.SourceIdentity.SourceRef = "10";
            u1.SourceIdentity.SourceSys = "KenyaEMR";
            u1.PersonNameInfo = TestPersonNameInfos()[0];

            var u2 = userInfos[1];
            u2.SourceIdentity = identities[1];
            u2.SourceIdentity.Source = "14080";
            u2.SourceIdentity.SourceRef = "11";
            u2.SourceIdentity.SourceSys = "KenyaEMR";
            u2.PersonNameInfo = TestPersonNameInfos()[1];

            var u3 = userInfos[2];
            u3.SourceIdentity = identities[2];
            u3.SourceIdentity.Source = "13023";
            u3.SourceIdentity.SourceRef = "10";
            u3.SourceIdentity.SourceSys = "IQCare";
            u3.PersonNameInfo = TestPersonNameInfos()[2];

            var u4 = userInfos[3];
            u4.SourceIdentity = identities[3];
            u4.SourceIdentity.Source = "13023";
            u4.SourceIdentity.SourceRef = "11";
            u4.SourceIdentity.SourceSys = "IQCare";
            u4.PersonNameInfo = TestPersonNameInfos()[3];

            return new List<UserInfo> {u1, u2, u3, u4};
        }
        public static List<ProviderInfo> TestProviderInfos()
        {
            if (_providerInfos.Count > 0) return _providerInfos;


            var userInfos = Builder<ProviderInfo>.CreateListOfSize(4)
                .All()
                .With(x=>x.ProviderTypeId=TestProviderTypes()[0].Id)
                .Build().ToList();
            var identities = Builder<SourceIdentity>.CreateListOfSize(4).Build().ToList();

            var p1 = userInfos[0];
            p1.SourceIdentity = identities[0];
            p1.SourceIdentity.Source = "14080";
            p1.SourceIdentity.SourceRef = "20";
            p1.SourceIdentity.SourceSys = "KenyaEMR";
            p1.PersonNameInfo = TestPersonNameInfos()[0];

            var p2 = userInfos[1];
            p2.SourceIdentity = identities[1];
            p2.SourceIdentity.Source = "14080";
            p2.SourceIdentity.SourceRef = "21";
            p2.SourceIdentity.SourceSys = "KenyaEMR";
            p2.PersonNameInfo = TestPersonNameInfos()[1];

            var p3 = userInfos[2];
            p3.SourceIdentity = identities[2];
            p3.SourceIdentity.Source = "13023";
            p3.SourceIdentity.SourceRef = "20";
            p3.SourceIdentity.SourceSys = "IQCare";
            p3.PersonNameInfo = TestPersonNameInfos()[2];

            var p4 = userInfos[3];
            p4.SourceIdentity = identities[3];
            p4.SourceIdentity.Source = "13023";
            p4.SourceIdentity.SourceRef = "21";
            p4.SourceIdentity.SourceSys = "IQCare";
            p4.PersonNameInfo = TestPersonNameInfos()[3];

            return new List<ProviderInfo> { p1, p2, p3, p4 };
        }

        public static List<Module> TestModules()
        {
            if (_modules.Count > 0) return _modules;

            var list = Builder<Module>.CreateListOfSize(1)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();
            return list;
        }
        public static List<Form> TestForms()
        {

            if (_forms.Count > 0) return _forms;

            var list = Builder<Form>.CreateListOfSize(2)
                .All()
                .With(x => x.Voided = false)
                .With(x => x.ModuleId = TestModules()[0].Id)
                .Build()
                .ToList();

            
            return list;
        }
        public static List<EncounterType> TestEncounterTypes()
        {
            if (_encounterTypes.Count > 0) return _encounterTypes;

            var list = Builder<EncounterType>.CreateListOfSize(2)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();
            return list;
        }

        public static List<Concept> TestConcepts()
        {
            if (_concepts.Count > 0) return _concepts;

            var list = Builder<Concept>.CreateListOfSize(4)
                .All()
                .With(x => x.Voided = false)
                .With(x=>x.CategoryId=null)
                .With(x => x.ConceptTypeId = TestConceptTypes()[0].Id)
                .Build()
                .ToList();
            return list;
        }

        public static List<Question> TestQuestions()
        {
            if (_questions.Count > 0) return _questions;

            var list = Builder<Question>.CreateListOfSize(4)
                .All()
                .With(x => x.Voided = false)
                .Build()
                .ToList();
            list[0].ConceptId = TestConcepts()[0].Id;
            list[0].FormId = TestForms()[0].Id;
            list[1].ConceptId = TestConcepts()[1].Id;
            list[1].FormId = TestForms()[0].Id;

            list[2].ConceptId = TestConcepts()[2].Id;
            list[2].FormId = TestForms()[1].Id;
            list[3].ConceptId = TestConcepts()[3].Id;
            list[3].FormId = TestForms()[1].Id;
            return list;
        }
        public static List<ClientInfo> TestClientInfos()
        {
            if (_clientInfos.Count > 0) return _clientInfos;

            var clientInfos = Builder<ClientInfo>.CreateListOfSize(4).Build().ToList();

            clientInfos[0].Id = TestClients()[0].Id;
            clientInfos[0].PracticeId = TestPracticeWithActivation()[0].Id;
            clientInfos[0].PracticeCode = "14080";
            clientInfos[0].Person = TestPersonInfos()[0];
            clientInfos[0].Person.Id = TestClients()[0].PersonId;
            clientInfos[0].Identifiers = new List<IdentifierInfo> { TestIdentifierInfos()[0]};

            clientInfos[1].Id = TestClients()[1].Id;
            clientInfos[1].PracticeId = TestPracticeWithActivation()[0].Id;
            clientInfos[1].PracticeCode = "14080";
            clientInfos[1].Person = TestPersonInfos()[1];
            clientInfos[1].Person.Id = TestClients()[1].PersonId;
            clientInfos[1].Identifiers = new List<IdentifierInfo> { TestIdentifierInfos()[1] };

            clientInfos[2].PracticeId = TestPracticeWithActivation()[1].Id;
            clientInfos[2].PracticeCode = "13023";
            clientInfos[2].Person = TestPersonInfos()[2];
            clientInfos[2].Identifiers = new List<IdentifierInfo> { TestIdentifierInfos()[2] };

            clientInfos[3].PracticeId = TestPracticeWithActivation()[1].Id;
            clientInfos[3].PracticeCode = "13023";
            clientInfos[3].Person = TestPersonInfos()[3];
            clientInfos[3].Identifiers = new List<IdentifierInfo> { TestIdentifierInfos()[3] };
            clientInfos[3].Relationship = new List<RelationshipInfo> { TestRelationshipInfos()[0] };

            return clientInfos;
        }

        public static List<Encounter> TestEncounters()
        {
            if (_encounters.Count > 0) return _encounters;
            
            var encounters = Builder<Encounter>.CreateListOfSize(1).Build().ToList();

            encounters[0].ClientId = TestClients()[0].Id;
            encounters[0].FormId = TestForms()[0].Id;
            encounters[0].EncounterTypeId = TestEncounterTypes()[0].Id;
            encounters[0].ProviderId = TestProviders()[0].Id;
            encounters[0].PracticeId = TestClients()[0].PracticeId;
            encounters[0].UserId = TestUsers()[0].Id;

            var obs = Builder<Obs>.CreateListOfSize(2).All().With(x => x.EncounterId = encounters[0].Id).Build().ToList();
            obs[0].QuestionId = TestQuestions()[0].Id;
            obs[1].QuestionId = TestQuestions()[1].Id;
            encounters[0].Obses = obs;

            return encounters;
        }


        public static List<PersonInfo> TestPersonInfos()
        {
            if (_personInfos.Count > 0) return _personInfos;

            var personInfos = Builder<PersonInfo>.CreateListOfSize(4).Build().ToList();

            personInfos[0].Addresses = new List<AddressInfo> {TestAddressInfos()[0]};
            personInfos[0].Contacts = new List<ContactInfo> {TestContactInfos()[0]};

            personInfos[1].Addresses = new List<AddressInfo> { TestAddressInfos()[1] };
            personInfos[1].Contacts = new List<ContactInfo> { TestContactInfos()[1] };

            personInfos[2].Addresses = new List<AddressInfo> { TestAddressInfos()[2] };
            personInfos[2].Contacts = new List<ContactInfo> { TestContactInfos()[2] };

            personInfos[3].Addresses = new List<AddressInfo> { TestAddressInfos()[3] };
            personInfos[3].Contacts = new List<ContactInfo> { TestContactInfos()[3] };

            return personInfos;
        }
        public static List<IdentifierInfo> TestIdentifierInfos()
        {
            if (_identifierInfos.Count > 0) return _identifierInfos;
            var identifierInfos = Builder<IdentifierInfo>.CreateListOfSize(4).All()
                .With(x => x.IdentifierTypeId = TestIdentifierTypes()[0].Id)
                .Build().ToList();
            return identifierInfos;
        }
        public static List<RelationshipInfo> TestRelationshipInfos()
        {
            if (_relationshipInfos.Count > 0) return _relationshipInfos;
            var relationshipInfos = Builder<RelationshipInfo>.CreateListOfSize(1)
                .All()
                .With(x => x.RelationshipTypeId = TestRelationshipTypes()[0].Id)
                .Build().ToList();
            return relationshipInfos;
        }
        public static List<AddressInfo> TestAddressInfos()
        {
            if (_addressInfos.Count > 0) return _addressInfos;
            var addressInfos = Builder<AddressInfo>.CreateListOfSize(4).All()
                .With(x=>x.CountyId=TestCounties()[0].Id)
                .Build().ToList();
            return addressInfos;
        }
        public static List<ContactInfo> TestContactInfos()
        {
            if (_contactInfos.Count > 0) return _contactInfos;
            var contactInfos = Builder<ContactInfo>.CreateListOfSize(4).Build().ToList();
            return contactInfos;
        }

        public static List<ObsInfo> TestObsInfos()
        {
            if (_obsInfos.Count > 0) return _obsInfos;
            var obsInfos = Builder<ObsInfo>.CreateListOfSize(2).Build().ToList();
            obsInfos[0].QuestionId = TestQuestions()[0].Id;
            obsInfos[0].EncounterId = TestEncounterInfos()[0].Id;
            obsInfos[1].QuestionId = TestQuestions()[1].Id;
            obsInfos[1].EncounterId = TestEncounterInfos()[0].Id;
            return obsInfos;
        }

        public static List<EncounterInfo> TestEncounterInfos()
        {
            if (_encounterInfos.Count > 0) return _encounterInfos;

            var obs = Builder<ObsInfo>.CreateListOfSize(4).All().Build().ToList();

            var encounterInfos = Builder<EncounterInfo>.CreateListOfSize(2).Build().ToList();

            encounterInfos[0].Id = TestEncounters()[0].Id;
            encounterInfos[0].ClientId = TestEncounters()[0].ClientId;
            encounterInfos[0].FormId = TestEncounters()[0].FormId;
            encounterInfos[0].EncounterTypeId = TestEncounters()[0].EncounterTypeId;
            encounterInfos[0].ProviderId = TestEncounters()[0].ProviderId;
            encounterInfos[0].PracticeId = TestEncounters()[0].PracticeId;
            encounterInfos[0].UserId = TestEncounters()[0].UserId;


            obs[0].EncounterId = TestEncounters()[0].Id;
            obs[0].Id = TestEncounters()[0].Obses.ToList()[0].Id;
            obs[0].QuestionId = TestEncounters()[0].Obses.ToList()[0].QuestionId;
            obs[1].EncounterId = TestEncounters()[0].Id;
            obs[1].Id = TestEncounters()[0].Obses.ToList()[1].Id;
            obs[1].QuestionId = TestEncounters()[0].Obses.ToList()[1].QuestionId;

            encounterInfos[0].Obses.Add(obs[0]);
            encounterInfos[0].Obses.Add(obs[1]);

            encounterInfos[1].ClientId = TestClients()[0].Id;
            encounterInfos[1].FormId = TestForms()[1].Id;
            encounterInfos[1].EncounterTypeId = TestEncounterTypes()[0].Id;
            encounterInfos[1].ProviderId = TestProviders()[0].Id;
            encounterInfos[1].PracticeId = TestClients()[0].PracticeId;
            encounterInfos[1].UserId = TestUsers()[0].Id;

            obs[2].EncounterId = encounterInfos[1].Id;
            obs[2].QuestionId = TestQuestions()[0].Id;
            obs[3].EncounterId = encounterInfos[1].Id;
            obs[3].QuestionId = TestQuestions()[1].Id;

            encounterInfos[1].Obses.Add(obs[2]);
            encounterInfos[1].Obses.Add(obs[3]);


            return encounterInfos;
        }

        public static ClientInfo TestClientInfo()
        {
            var json= @"
{
  ^Id^: ^4700b0e0-00c0-0c0f-0d0a-a0b0000df000^,
  ^MaritalStatus^: ^MM^,
  ^KeyPop^: ^NA^,
  ^OtherKeyPop^: ^^,
  ^PracticeId^: ^ab054358-98b9-11e7-abc4-cec278b6b50a^,
  ^PracticeCode^: null,
  ^Person^: {
    ^FirstName^: ^Julie^,
    ^MiddleName^: ^^,
    ^LastName^: ^Swagger^,
    ^Gender^: ^F^,
    ^BirthDate^: ^/Date(389048400000+0300)/^,
    ^BirthDateEstimated^: false,
    ^Email^: ^jswagger@gmail.com^,
    ^Addresses^: [
      {
        ^Landmark^: ^Kibera School^,
        ^CountyId^: 47,
        ^Preferred^: true,
        ^PersonId^: ^82dfdc68-6c3c-4a39-8f1f-a7b7016df22e^,
        ^Id^: ^a2127fa6-7776-11e7-b5a5-be2e44b06b34^,
        ^Voided^: false
      }
    ],
    ^Contacts^: [
      {
        ^Phone^: 134021121,
        ^Preferred^: true,
        ^PersonId^: ^82dfdc68-6c3c-4a39-8f1f-a7b7016df22e^,
        ^Id^: ^a21271a8-7776-11e7-b5a5-be2e44b06b34^,
        ^Voided^: false
      }
    ],
    ^Id^: ^82dfdc68-6c3c-4a39-8f1f-a7b7016df22e^,
    ^Voided^: false
  },
  ^Identifiers^: [
    {
      ^IdentifierTypeId^: ^Serial^,
      ^Identifier^: ^201707001^,
      ^RegistrationDate^: ^2017JUL01^,
      ^Preferred^: true,
      ^ClientId^: ^4547b7e0-98c7-4c6f-9d2a-a7b7016df232^,
      ^Id^: ^7e61629e-6b99-11e7-907b-a6006ad4dba0^,
      ^Voided^: false
    }
  ],
  ^Relationships^: [
    {
      ^RelationshipTypeId^: ^Partner^,
      ^RelatedClientId^: ^4547b7e0-98c7-4c6f-9d2a-a7b7016df232^,
      ^Preferred^: true,
      ^ClientId^: ^4547b7e0-98c7-4c6f-9d2a-a7b7016df234^,
      ^Id^: ^7e51629e-6b99-11e7-907b-a6006ad4dba0^,
      ^Voided^: false
    }
  ]
}
".Replace("^","'");
            return JsonConvert.DeserializeObject<ClientInfo>(json);
        }
    }
}