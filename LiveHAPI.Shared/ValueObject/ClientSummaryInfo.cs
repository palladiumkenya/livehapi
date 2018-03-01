using System;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ClientSummaryInfo : IClientSummary
    {
        public Guid Id { get; set; }
        public string Area { get; set; }
        public Guid ClientId { get; set; }
        public decimal Rank { get; set; }
        public string Report { get; set; }
        public DateTime? ReportDate { get; set; }

        public ClientSummaryInfo()
        {
        }

        public ClientSummaryInfo(Guid id, string area, Guid clientId, decimal rank, string report, DateTime? reportDate)
        {
            Id = id;
            Area = area;
            ClientId = clientId;
            Rank = rank;
            Report = report;
            ReportDate = reportDate;
        }
    }
}