using System;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ObsInfo : IObs
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public DateTime ObsDate { get; set; }
        public string ValueText { get; set; }
        public decimal? ValueNumeric { get; set; }
        public Guid? ValueCoded { get; set; }
        public string ValueMultiCoded { get; set; }
        public DateTime? ValueDateTime { get; set; }

        public Guid ClientId { get; set; }

        public Guid EncounterId { get; set; }

    }
}