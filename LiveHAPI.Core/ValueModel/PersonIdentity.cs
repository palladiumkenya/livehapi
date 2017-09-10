using System.ComponentModel.DataAnnotations;

namespace LiveHAPI.Core.ValueModel
{
    public class PersonIdentity
    {    
        public string Source { get; set; }
        public string SourceRef { get; set; }
        public string SourceSys { get; set; }

        public PersonIdentity()
        {
        }

        public PersonIdentity(string source, string sourceRef, string sourceSys)
        {
            Source = source;
            SourceRef = sourceRef;
            SourceSys = sourceSys;
        }
    }
}