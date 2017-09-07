using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class QuestionReValidation : Entity<Guid>
    {
        [MaxLength(50)]
        public string ConditionId { get; set; }
        
        public Guid? RefQuestionId { get; set; }
        [MaxLength(50)]
        public string ResponseType { get; set; }
        [MaxLength(50)]
        public string Response { get; set; }
        [MaxLength(100)]
        public string ResponseComplex { get; set; }
        public decimal? Group { get; set; }
        [MaxLength(50)]
        public string ActionId { get; set; }
        
        public Guid QuestionValidationId { get; set; }
        public Guid QuestionId { get; set; }

        public QuestionReValidation()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{ConditionId} {ActionId} {QuestionValidationId}";
        }
    }
}