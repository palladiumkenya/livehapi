using LiveHAPI.Core.Model.Encounters;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.Network;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Studio;
using Microsoft.EntityFrameworkCore;


namespace LiveHAPI.Infrastructure
{
    public class LiveHAPIContext : DbContext
    {
        public LiveHAPIContext(DbContextOptions<LiveHAPIContext> options) 
            : base(options)
        {
           Database.Migrate();
        }

        public DbSet<MasterFacility> MasterFacilities { get; set; }
        public DbSet<PracticeType> PracticeTypes { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<IdentifierType> IdentifierTypes { get; set; }
        public DbSet<ProviderType> ProviderTypes { get; set; }
        public DbSet<ValidatorType> ValidatorTypes { get; set; }
        public DbSet<Validator> Validators { get; set; }
        public DbSet<ConceptType> ConceptTypes { get; set; }
        public DbSet<KeyPop> KeyPops { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Condition> Conditions { get; set; }

        public DbSet<County> Counties { get; set; }
        public DbSet<SubCounty> SubCounties { get; set; }

        public DbSet<Practice> Practices { get; set; }
        public DbSet<PracticeActivation> PracticeActivations { get; set; }
        
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonAddress> PersonAddresss { get; set; }
        public DbSet<PersonContact> PersonContacts { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<ClientAttribute> ClientAttributes { get; set; }
        public DbSet<ClientIdentifier> ClientIdentifiers { get; set; }
        public DbSet<ClientRelationship> ClientRelationships { get; set; }

        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<Obs> Obses { get; set; }
        public DbSet<ObsFinalTestResult> ObsFinalTestResults { get; set; }
        public DbSet<ObsLinkage> ObsLinkages { get; set; }
        public DbSet<ObsTestResult> ObsTestResults { get; set; }
        public DbSet<ObsTraceResult> ObsTraceResults { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Concept> Concepts { get; set; }

        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionBranch> QuestionBranches { get; set; }
        public DbSet<QuestionRemoteTransformation> QuestionRemoteTransformations { get; set; }
        public DbSet<QuestionReValidation> QuestionReValidations { get; set; }
        public DbSet<QuestionTransformation> QuestionTransformation { get; set; }
        public DbSet<QuestionValidation> QuestionValidations { get; set; }

        public DbSet<Module> Modules { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormImplementation> FormImplementations { get; set; }
        public DbSet<FormProgram> Programs { get; set; }
    }
}