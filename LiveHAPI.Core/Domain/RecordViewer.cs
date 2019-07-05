using System.Collections.Generic;

namespace LiveHAPI.Core.Domain
{
    public class FileViewer
    {
        public string Title { get; set; }
        public List<RecordViewer> Records { get; set; }
    }
    public class RecordViewer
    {

        public string Title { get; set; }
    }
}
