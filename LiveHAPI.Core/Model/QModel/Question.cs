using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.QModel
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
        public Guid FormId { get; set; }

        public ICollection<QuestionValidation> Validations { get; set; } = new List<QuestionValidation>();
        public ICollection<QuestionBranch> Branches { get; set; } = new List<QuestionBranch>();

        public ICollection<QuestionReValidation> ReValidations { get; set; } = new List<QuestionReValidation>();
        public ICollection<QuestionTransformation> Transformations { get; set; } = new List<QuestionTransformation>();
        public ICollection<QuestionRemoteTransformation> RemoteTransformations { get; set; } =new List<QuestionRemoteTransformation>();
        
      
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