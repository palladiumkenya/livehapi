using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class ClientSummary : Entity<Guid>
    {
        public string Area { get; set; }
        public string Report { get; set; }
        public DateTime? ReportDate { get; set; }
        public Decimal Rank { get; set; }

        public ClientSummary()
        {
            Id = LiveGuid.NewGuid();
        }

        public ClientSummary(string area, string report, DateTime? reportDate, decimal rank) : this()
        {
            Area = area;
            Report = report;
            ReportDate = reportDate;
            Rank = rank;
        }

        public override string ToString()
        {
            return $"{Area} | {Report} | {ReportDate?.Date} |{Rank}";
        }
    }
}