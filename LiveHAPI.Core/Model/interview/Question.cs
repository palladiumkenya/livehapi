using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Question : Entity<Guid>
    {
        public Guid ConceptId { get; set; }
        [MaxLength(50)]
        public string Ordinal { get; set; }
        [MaxLength(50)]
        public string Display { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        public Decimal Rank { get; set; }

        
        public List<QuestionValidation> Validations { get; set; } = new List<QuestionValidation>();

        
        public List<QuestionReValidation> ReValidations { get; set; } = new List<QuestionReValidation>();

        
        public List<QuestionBranch> Branches { get; set; } = new List<QuestionBranch>();
        
        public List<QuestionTransformation> Transformations { get; set; } = new List<QuestionTransformation>();

        
        public List<QuestionRemoteTransformation> RemoteTransformations { get; set; } =
            new List<QuestionRemoteTransformation>();

        
        public Guid FormId { get; set; }

        
      
        public Question()
        {
            Id = LiveGuid.NewGuid();
        }
        
        public override string ToString()
        {
            return $"{Ordinal}. {Display}";
        }
    }
}