using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class QuestionRemoteTransformation : Entity<Guid>
    {
        [MaxLength(50)]
        public string ConditionId { get; set; }
        [MaxLength(50)]
        public string ClientAttributeId { get; set; }
        
        public Guid? RemoteQuestionId { get; set; }
        
        public Guid? SelfQuestionId { get; set; }
        [MaxLength(50)]
        public string ResponseType { get; set; }
        [MaxLength(50)]
        public string Response { get; set; }
        [MaxLength(50)]
        public string ResponseComplex { get; set; }
        public decimal? Group { get; set; }
        [MaxLength(50)]
        public string ActionId { get; set; }
        [MaxLength(50)]
        public string Content { get; set; }
        [MaxLength(50)]
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