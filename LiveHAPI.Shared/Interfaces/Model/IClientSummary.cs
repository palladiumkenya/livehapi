using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IClientSummary
    {
        Guid Id { get; set; }
        string Area { get; set; }
        Guid ClientId { get; set; }
        decimal Rank { get; set; }
        string Report { get; set; }
        DateTime? ReportDate { get; set; }
    }
}