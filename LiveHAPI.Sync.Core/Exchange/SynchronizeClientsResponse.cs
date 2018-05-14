using System.Collections.Generic;

namespace LiveHAPI.Sync.Core.Exchange
{
    public class SynchronizeClientsResponse
    {
        public string Value { get; set; }
        public List<ErrorResponse> ErrorResponses { get; set; }=new List<ErrorResponse>();
        public bool IsValid { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
