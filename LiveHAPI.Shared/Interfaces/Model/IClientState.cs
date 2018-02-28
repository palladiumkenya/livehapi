using System;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public interface IClientState
    {
        Guid ClientId { get; set; }
        Guid? EncounterId { get; set; }
        Guid? IndexClientId { get; set; }
        LiveState Status { get; set; }
        DateTime StatusDate { get; set; }
    }
}