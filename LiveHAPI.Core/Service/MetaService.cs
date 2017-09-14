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
            return _lookupRepository.ReadAll<County>(x=>x.SubCounties);
        }

        public IEnumerable<Category> ReadLookupCategories()
        {
            return _lookupRepository.ReadAll<Category>();
        }

        public IEnumerable<Item> ReadLookupItems()
        {
            return _lookupRepository.ReadAll<Item>();
        }

        public IEnumerable<CategoryItem> ReadLookupCategoriesItems()
        {
            return _lookupRepository.ReadAll<CategoryItem>();
        }

        public IEnumerable<SubCounty> ReadSubCounties()
        {
            return _lookupRepository.ReadAll<SubCounty>();
        }

        public IEnumerable<PracticeType> ReadPracticeTypes()
        {
            return _lookupRepository.ReadAll<PracticeType>();
        }

        public IEnumerable<IdentifierType> ReadIdentifierTypes()
        {
            return _lookupRepository.ReadAll<IdentifierType>();
        }

        public IEnumerable<RelationshipType> ReadRelationshipTypes()
        {
            return _lookupRepository.ReadAll<RelationshipType>();
        }

        public IEnumerable<KeyPop> ReadKeyPops()
        {
            return _lookupRepository.ReadAll<KeyPop>();
        }

        public IEnumerable<MaritalStatus> ReadMaritalStatuses()
        {
            return _lookupRepository.ReadAll<MaritalStatus>();
        }

        public IEnumerable<ProviderType> ReadProviderTypes()
        {
            return _lookupRepository.ReadAll<ProviderType>();
        }

        public IEnumerable<Action> ReadActions()
        {
            return _lookupRepository.ReadAll<Action>();
        }

        public IEnumerable<Condition> ReadConditions()
        {
            return _lookupRepository.ReadAll<Condition>();
        }

        public IEnumerable<ConceptType> ReadConceptTypes()
        {
            return _lookupRepository.ReadAll<ConceptType>();
        }

        public IEnumerable<ValidatorType> ReadValidatorTypes()
        {
            return _lookupRepository.ReadAll<ValidatorType>();
        }

        public IEnumerable<Validator> ReadValidators()
        {
            return _lookupRepository.ReadAll<Validator>();
        }

        public IEnumerable<EncounterType> ReadEncounterTypes()
        {
            return _lookupRepository.ReadAll<EncounterType>();
        }
    }
}