using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LiveHAPI.Sync.Core.Exchange
{
    public class SynchronizeClientsResponse
    {
        public string Value { get; set; }
        public List<ErrorResponse> Errors { get; set; }=new List<ErrorResponse>();
        public bool IsValid { get; set; }

        [JsonIgnore]
        public string ErrorMessage
        {
            get
            {
                if (Errors.Any())
                {
                    return string.Join('|', Errors);
                }
                return string.Empty;
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
