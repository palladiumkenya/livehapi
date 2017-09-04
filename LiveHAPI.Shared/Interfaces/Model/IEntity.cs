using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IEntity<TId> : IEquatable<IEntity<TId>>
    {
        TId Id { get; set; }
        bool Voided { get; set; }
    }
}