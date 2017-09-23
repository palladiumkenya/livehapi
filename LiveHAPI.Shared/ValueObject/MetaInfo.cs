using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.ValueObject.Meta;

namespace LiveHAPI.Shared.ValueObject
{
  public  class MetaInfo
    {
        public List<PracticeTypeInfo> PracticeTypes { get; set; }
        public List<IdentifierTypeInfo> IdentifierTypes { get; set; }
        public List<RelationshipTypeInfo> RelationshipTypes { get; set; }
        public List<KeyPopInfo> KeyPops { get; set; }
        public List<MaritalStatusInfo> MaritalStatuses { get; set; }
        public List<ProviderTypeInfo> ProviderTypes { get; set; }
        public List<ActionInfo> Actions { get; set; }
        public List<ConditionInfo> Conditions { get; set; }
        public List<ConceptTypeInfo> ConceptTypes { get; set; }
        public List<ValidatorTypeInfo> ValidatorTypes { get; set; }
        public List<ValidatorInfo> Validators { get; set; }
        public List<EncounterTypeInfo> EncounterTypes { get; set; }
    }
}
