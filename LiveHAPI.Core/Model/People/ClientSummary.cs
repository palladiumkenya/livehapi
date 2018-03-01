using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class ClientSummary : Entity<Guid>, IClientSummary
    {
        [MaxLength(100)]
        public string Area { get; set; }
        [MaxLength(100)]
        public string Report { get; set; }
        public DateTime? ReportDate { get; set; }
        public Decimal Rank { get; set; }
        public Guid ClientId { get; set; }

        public ClientSummary()
        {
            Id = LiveGuid.NewGuid();
        }

        public ClientSummary(string area, string report, DateTime? reportDate, decimal rank,Guid clientId) : this()
        {
            Area = area;
            Report = report;
            ReportDate = reportDate;
            Rank = rank;
            ClientId = clientId;
        }

        public override string ToString()
        {
            return $"{Area} | {Report} | {ReportDate?.Date} |{Rank}";
        }
    }
}