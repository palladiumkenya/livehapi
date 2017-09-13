using System;
using System.Collections.Generic;
using LiveHAPI.Core.Interfaces.Repository;
using LiveHAPI.Core.Interfaces.Services;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Core.Model.QModel;
using LiveHAPI.Core.Model.Studio;
using Action = LiveHAPI.Core.Model.QModel.Action;

namespace LiveHAPI.Core.Service
{
    public class MetaService:IMetaService
    {

        private readonly ILookupRepository _lookupRepository;

        public MetaService(ILookupRepository lookupRepository)
        {
            _lookupRepository = lookupRepository;
        }

        public IEnumerable<County> ReadCounties()
        {
            return _lookupRepository.ReadAll<County>();
        }

        public IEnumerable<Category> ReadLookupCategories(Guid practiceId)
        {
            return _lookupRepository.ReadAll<Category>();
        }

        public IEnumerable<Item> ReadLookupItems(Guid practiceId)
        {
            return _lookupRepository.ReadAll<Item>();
        }

        public IEnumerable<CategoryItem> ReadLookupCategoriesItems(Guid practiceId)
        {
            return _lookupRepository.ReadAll<CategoryItem>();
        }

        public IEnumerable<SubCounty> ReadSubCounties(Guid practiceId)
        {
            return _lookupRepository.ReadAll<SubCounty>();
        }

        public IEnumerable<PracticeType> ReadPracticeTypes(Guid practiceId)
        {
            return _lookupRepository.ReadAll<PracticeType>();
        }

        public IEnumerable<IdentifierType> ReadIdentifierTypes(Guid practiceId)
        {
            return _lookupRepository.ReadAll<IdentifierType>();
        }

        public IEnumerable<RelationshipType> ReadRelationshipTypes(Guid practiceId)
        {
            return _lookupRepository.ReadAll<RelationshipType>();
        }

        public IEnumerable<KeyPop> ReadKeyPops(Guid practiceId)
        {
            return _lookupRepository.ReadAll<KeyPop>();
        }

        public IEnumerable<MaritalStatus> ReadMaritalStatuses(Guid practiceId)
        {
            return _lookupRepository.ReadAll<MaritalStatus>();
        }

        public IEnumerable<ProviderType> ReadProviderTypes(Guid practiceId)
        {
            return _lookupRepository.ReadAll<ProviderType>();
        }

        public IEnumerable<Action> ReadActions(Guid practiceId)
        {
            return _lookupRepository.ReadAll<Action>();
        }

        public IEnumerable<Condition> ReadConditions(Guid practiceId)
        {
            return _lookupRepository.ReadAll<Condition>();
        }

        public IEnumerable<ConceptType> ReadConceptTypes(Guid practiceId)
        {
            return _lookupRepository.ReadAll<ConceptType>();
        }

        public IEnumerable<ValidatorType> ReadValidatorTypes(Guid practiceId)
        {
            return _lookupRepository.ReadAll<ValidatorType>();
        }

        public IEnumerable<Validator> ReadValidators(Guid practiceId)
        {
            return _lookupRepository.ReadAll<Validator>();
        }

        public IEnumerable<EncounterType> ReadEncounterTypes(Guid practiceId)
        {
            return _lookupRepository.ReadAll<EncounterType>();
        }
    }
}