using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class QuestionBranch : Entity<Guid>
    {
        
        public string ConditionId { get; set; }
        
        public Guid? RefQuestionId { get; set; }
        public string ResponseType { get; set; }
        public string Response { get; set; }
        public string ResponseComplex { get; set; }
        public decimal? Group { get; set; }
        
        public string ActionId { get; set; }
        
        public Guid? GotoQuestionId { get; set; }
        
        public Guid QuestionId { get; set; }

        public QuestionBranch()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{ConditionId},{ResponseType}{Response}>>{GotoQuestionId}";
        }
        public Guid? Evaluate(ObsValue current)
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

                return responseObject.Equals(current.Value) ? GotoQuestionId : null;
            }
            return null;
        }

        //TODO:Pre Branches Evaluate
        public Guid? Evaluate(ObsValue other,ObsValue current)
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

                return responseObject.Equals(current.Value) ? GotoQuestionId : null;
            }
            return null;
        }

    }
}