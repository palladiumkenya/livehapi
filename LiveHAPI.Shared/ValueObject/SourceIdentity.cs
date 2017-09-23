using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class SourceIdentity: ISourceIdentity
    {    
        /// <summary>
        /// e.g Facility Code
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Record Id
        /// </summary>
        public string SourceRef { get; set; }
        /// <summary>
        /// e.g IQCare
        /// </summary>
        public string SourceSys { get; set; }

        public SourceIdentity()
        {
        }

        public SourceIdentity(string source, string sourceRef, string sourceSys)
        {
            Source = source;
            SourceRef = sourceRef;
            SourceSys = sourceSys;
        }
    }
}