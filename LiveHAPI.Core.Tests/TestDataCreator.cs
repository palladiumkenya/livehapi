using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Infrastructure;
using LiveHAPI.Shared.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;

namespace LiveHAPI.Core.Tests
{
    public class TestDataCreator
    {
        public static void Init(LiveHAPIContext context)
        {
            var counties = TestData.TestCounties();
            var facs = TestData.TestFacilities();
            var subCounties = TestData.TestSubCounties();
            var practiceTypes = TestData.TestPracticeTypes();
            var providerTypes = TestData.TestProviderTypes();
            var identifierTypes = TestData.TestIdentifierTypes();
            var relationshipTypes = TestData.TestRelationshipTypes();
            var conceptTypes = TestData.TestConceptTypes();
            var encounterTypes = TestData.TestEncounterTypes();

            var modules = TestData.TestModules();
            var forms = TestData.TestForms();
            var concepts = TestData.TestConcepts();
            var questions = TestData.TestQuestions();

            var practices = TestData.TestPracticeWithActivation();
            var practiceActivations = practices.SelectMany(x => x.Activations).ToList();
            var persons = TestData.TestPersons();
            var personNames = persons.SelectMany(x => x.Names).ToList();
            var personAddresses = persons.SelectMany(x => x.Addresses).ToList();
            var personContacts = persons.SelectMany(x => x.Contacts).ToList();

            var users = TestData.TestUsers();
            var providers = TestData.TestProviders();
            var clients = TestData.TestClients();
            var clientIdentifiers = clients.SelectMany(x => x.Identifiers).ToList();
            var clientRelationships = clients.SelectMany(x => x.Relationships).ToList();

            var encounters = TestData.TestEncounters();
            var obses = encounters.SelectMany(x => x.Obses).ToList();

            Clear(context);
            Create(context,
                counties, facs, subCounties, practiceTypes, relationshipTypes, providerTypes, identifierTypes,
                conceptTypes, encounterTypes,
                modules, forms, concepts, questions,
                practices, practiceActivations,
                persons, personNames, personAddresses, personContacts,
                users,
                providers,
                clients, clientIdentifiers, clientRelationships,encounters,obses);
        }

        public static void Create(LiveHAPIContext context, params IEnumerable<object>[] entities)
        {
            foreach (IEnumerable<object> t in entities)
            {
                context.AddRange(t);
            }
            context.SaveChanges();
        }
        public static void Clear(LiveHAPIContext context, params DbSet<object>[] entities)
        {

            context.RemoveRange(context.Obses);
            context.RemoveRange(context.Encounters);
            context.RemoveRange(context.Providers);
            context.RemoveRange(context.Users);
            context.RemoveRange(context.PersonNames);
            context.RemoveRange(context.PersonContacts);
            context.RemoveRange(context.PersonAddresss);
            context.RemoveRange(context.Persons);
            context.RemoveRange(context.ClientRelationships);
            context.RemoveRange(context.ClientIdentifiers);
            context.RemoveRange(context.Clients);
            context.RemoveRange(context.PracticeActivations);
            context.RemoveRange(context.Practices);
            context.RemoveRange(context.PracticeTypes);
            context.RemoveRange(context.ProviderTypes);
            context.RemoveRange(context.IdentifierTypes);
            context.RemoveRange(context.RelationshipTypes);

            context.RemoveRange(context.Forms);
            context.RemoveRange(context.Questions);
            context.RemoveRange(context.Concepts);
            context.RemoveRange(context.ConceptTypes);
            context.RemoveRange(context.EncounterTypes);
            context.RemoveRange(context.Modules);

            context.RemoveRange(context.SubCounties);
            context.RemoveRange(context.MasterFacilities);
            context.RemoveRange(context.Counties);
            context.SaveChanges();
        }
    }
}
