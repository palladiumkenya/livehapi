using System;
using System.Collections.Generic;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Studio;
using LiveHAPI.Shared.ValueObject;


namespace LiveHAPI.Core.Interfaces.Services
{
    public interface IMetaService
    {
        IEnumerable<County> ReadCounties();

                IEnumerable<Category> ReadLookupCategories(Guid practiceId);
                IEnumerable<Item> ReadLookupItems(Guid practiceId);
                IEnumerable<CategoryItem> ReadLookupCategoriesItems(Guid practiceId);
        

                IEnumerable<SubCounty> ReadSubCounties(Guid practiceId);
                IEnumerable<PracticeType> ReadPracticeTypes(Guid practiceId);
        
                IEnumerable<IdentifierType> ReadIdentifierTypes(Guid practiceId);
                IEnumerable<RelationshipType> ReadRelationshipTypes(Guid practiceId);
                IEnumerable<KeyPop> ReadKeyPops(Guid practiceId);
                IEnumerable<MaritalStatus> ReadMaritalStatuses(Guid practiceId);
                IEnumerable<ProviderType> ReadProviderTypes(Guid practiceId);
        
                IEnumerable<Model.QModel.Action> ReadActions(Guid practiceId);
                IEnumerable<Condition> ReadConditions(Guid practiceId);
                IEnumerable<ConceptType> ReadConceptTypes(Guid practiceId);
                IEnumerable<ValidatorType> ReadValidatorTypes(Guid practiceId);
                IEnumerable<Validator> ReadValidators(Guid practiceId);
        
                IEnumerable<EncounterType> ReadEncounterTypes(Guid practiceId);
    }
}