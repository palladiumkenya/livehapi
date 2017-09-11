using System;

namespace LiveHAPI.Core.Model.People
{
    public interface IClientAttribute
    {
        string Name { get; set; }
        Guid ClientId { get; set; }
    }
}