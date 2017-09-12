using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IClientAttribute
    {
        string Name { get; set; }
        Guid ClientId { get; set; }
    }
}