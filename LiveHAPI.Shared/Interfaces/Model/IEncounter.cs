using System;
using LiveHAPI.Shared.Enum;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IEncounter
    {
        Guid ClientId { get; set; }
        Guid? IndexClientId { get; set; }
        Guid FormId { get; set; }
        Guid EncounterTypeId { get; set; }
        DateTime EncounterDate { get; set; }
        VisitType VisitType { get; set; }
        Guid ProviderId { get; set; }
        Guid DeviceId { get; set; }
        Guid PracticeId { get; set; }
        DateTime? Started { get; set; }
        DateTime? Stopped { get; set; }
        bool IsComplete { get; set; }
        Guid UserId { get; set; }
    }
}