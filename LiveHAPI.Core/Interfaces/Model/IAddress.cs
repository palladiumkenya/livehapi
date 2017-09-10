using System;

namespace LiveHAPI.Core.Interfaces.Model
{
    public interface IAddress
    {
        string Landmark { get; set; }
        int? CountyId { get; set; }
        decimal? Lat { get; set; }
        decimal? Lng { get; set; }
        string Source { get; set; }
        string SourceRef { get; set; }
        string SourceSys { get; set; }
    }
}