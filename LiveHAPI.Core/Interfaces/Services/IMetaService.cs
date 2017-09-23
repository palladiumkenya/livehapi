using System.Collections.Generic;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Studio;


namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IMetaService
    {
        IEnumerable<County> ReadCounties();

                IEnumerable<Category> ReadLookupCategories();
                IEnumerable<Item> ReadLookupItems();
                IEnumerable<CategoryItem> ReadLookupCategoriesItems();
        

                IEnumerable<SubCounty> ReadSubCounties();
                IEnumerable<PracticeType> ReadPracticeTypes();
        
                IEnumerable<IdentifierType> ReadIdentifierTypes();
                IEnumerable<RelationshipType> ReadRelationshipTypes();
                IEnumerable<KeyPop> ReadKeyPops();
                IEnumerable<MaritalStatus> ReadMaritalStatuses();
                IEnumerable<ProviderType> ReadProviderTypes();
        
                IEnumerable<Model.QModel.Action> ReadActions();
                IEnumerable<Condition> ReadConditions();
                IEnumerable<ConceptType> ReadConceptTypes();
                IEnumerable<ValidatorType> ReadValidatorTypes();
                IEnumerable<Validator> ReadValidators();
        
                IEnumerable<EncounterType> ReadEncounterTypes();
    }
}