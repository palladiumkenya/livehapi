using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class QuestionRemoteTransformation : Entity<Guid>
    {
        
        public string ConditionId { get; set; }
        
        public string ClientAttributeId { get; set; }
        
        public Guid? RemoteQuestionId { get; set; }
        
        public Guid? SelfQuestionId { get; set; }
        public string ResponseType { get; set; }
        public string Response { get; set; }
        public string ResponseComplex { get; set; }
        public decimal? Group { get; set; }
        
        public string ActionId { get; set; }
        public string Content { get; set; }
        public string  AltContent { get; set; }
        
        public Guid QuestionId { get; set; }

        public QuestionRemoteTransformation()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{ConditionId} {ClientAttributeId} {ActionId}";
        }

   

    }
}