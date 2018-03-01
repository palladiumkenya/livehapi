using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class UserSummary : Entity<Guid>
    {
        [MaxLength(100)]
        public string Area { get; set; }
        public int Report { get; set; }
        public Decimal Rank { get; set; }
        public Guid UserId { get; set; }

        public UserSummary()
        {
            Id = LiveGuid.NewGuid();
        }

        public UserSummary(string area, int report,decimal rank,Guid userId) : this()
        {
            Area = area;
            Report = report;
            Rank = rank;
            UserId = userId;
        }

        public override string ToString()
        {
            return $"{Area} | {Report} | {Rank}";
        }
    }
}