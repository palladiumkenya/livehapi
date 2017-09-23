using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IObs
    {
        Guid QuestionId { get; set; }
        DateTime ObsDate { get; set; }
        string ValueText { get; set; }
        decimal? ValueNumeric { get; set; }
        Guid? ValueCoded { get; set; }
        string ValueMultiCoded { get; set; }
        DateTime? ValueDateTime { get; set; }
        Guid EncounterId { get; set; }
    }
}