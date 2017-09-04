using System;
using System.Collections.Generic;
using System.Linq;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Question : Entity<Guid>
    {
        
        public Guid ConceptId { get; set; }

        
        public Concept Concept { get; set; }

        public string Ordinal { get; set; }
        public string Display { get; set; }
        public string Description { get; set; }
        public Decimal Rank { get; set; }

        
        public List<QuestionValidation> Validations { get; set; } = new List<QuestionValidation>();

        
        public List<QuestionReValidation> ReValidations { get; set; } = new List<QuestionReValidation>();

        
        public List<QuestionBranch> Branches { get; set; } = new List<QuestionBranch>();
        
        public List<QuestionTransformation> Transformations { get; set; } = new List<QuestionTransformation>();

        
        public List<QuestionRemoteTransformation> RemoteTransformations { get; set; } =
            new List<QuestionRemoteTransformation>();

        
        public Guid FormId { get; set; }

        
        public bool HasValidations
        {
            get { return null != Validations && Validations.Any(x=>x.Revision==0); }
        }
        
        public bool IsRequired
        {
            get { return HasValidations && Validations.Any(x => x.ValidatorId == "Required"); }
        }
        
        public bool HasReValidations
        {
            get { return null != ReValidations && ReValidations.Any(); }
        }
        
        public bool HasBranches
        {
            get { return null != Branches && Branches.Any(); }
        }
        
        public bool HasTransformations
        {
            get { return null != Transformations && Transformations.Any(); }
        }
        
        public bool HasRemoteTransformations
        {
            get { return null != RemoteTransformations && RemoteTransformations.Any(); }
        }
        
        public List<Guid> SkippedQuestionIds { get; set; } =new List<Guid>();
        
        public List<SetResponse> SetResponses { get; set; } = new List<SetResponse>();

        
        public List<Guid> BlockedQuestionIds { get; set; } = new List<Guid>();


        public Question()
        {
            Id = LiveGuid.NewGuid();
        }

        public bool HasConditionalBranches(string condition)
        {
            return null != Branches && Branches.Any(x => x.ConditionId.ToLower() == condition.ToLower());
        }

        public bool HasConditionalTransformations(string condition)
        {
            return null != Transformations && Transformations.Any(x => x.ConditionId.ToLower() == condition.ToLower());
        }

        public override string ToString()
        {
            return $"{Ordinal}. {Display}";
        }
    }
}