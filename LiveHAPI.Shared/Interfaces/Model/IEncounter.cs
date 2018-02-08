using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IEncounter
    {
        Guid ClientId { get; set; }
        Guid FormId { get; set; }
        Guid EncounterTypeId { get; set; }
        DateTime EncounterDate { get; set; }
        Guid ProviderId { get; set; }
        Guid DeviceId { get; set; }
        Guid PracticeId { get; set; }
        DateTime? Started { get; set; }
        DateTime? Stopped { get; set; }
        bool IsComplete { get; set; }
        Guid UserId { get; set; }
    }
}