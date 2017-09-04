using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class QuestionTransformation : Entity<Guid>
    {
        
        public string ConditionId { get; set; }
        
        public Guid? RefQuestionId { get; set; }
        public string ResponseType { get; set; }
        public string Response { get; set; }
        public string ResponseComplex { get; set; }
        public decimal? Group { get; set; }
        
        public string ActionId { get; set; }
        public decimal? Rank { get; set; }
        public string Content { get; set; }
        
        public Guid QuestionId { get; set; }

        public QuestionTransformation()
        {
            Id = LiveGuid.NewGuid();
        }

        public SetResponse Evaluate(ObsValue current)
        {
            if (ResponseType.Equals("="))
            {
                object responseObject = Response;

                if (current.Type == typeof(Guid?))
                {
                    responseObject = new Guid(Response);
                }
                if (current.Type == typeof(decimal?))
                {
                    responseObject = Convert.ToDecimal(Response);
                }
                if (current.Type == typeof(DateTime?))
                {
                    responseObject = Convert.ToDateTime(Response);
                }

                return responseObject.Equals(current.Value) ? new SetResponse(RefQuestionId,ActionId,Content, Rank, ConditionId, ResponseComplex) : null;
            }
            return null;
        }

        public SetResponse GetComplex()
        {
            return new SetResponse(RefQuestionId, ActionId, Content, Rank, ConditionId, ResponseComplex);
        }

        public override string ToString()
        {
            return $"{ConditionId},{RefQuestionId}{ResponseType}{Response}{ActionId}";
        }
    }
}