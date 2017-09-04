using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class QuestionReValidation : Entity<Guid>
    {
        
        public string ConditionId { get; set; }
        
        public Guid? RefQuestionId { get; set; }
        public string ResponseType { get; set; }
        public string Response { get; set; }
        public string ResponseComplex { get; set; }
        public decimal? Group { get; set; }
        
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