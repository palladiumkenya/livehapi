using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IEncounterType
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}