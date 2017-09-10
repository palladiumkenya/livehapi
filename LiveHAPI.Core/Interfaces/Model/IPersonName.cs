using System;

namespace LiveHAPI.Core.Interfaces.Model
{
    public interface IPersonName
    {
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string MothersName { get; set; }
        string Source { get; set; }
        string SourceRef { get; set; }
        string SourceSys { get; set; }
    }
}