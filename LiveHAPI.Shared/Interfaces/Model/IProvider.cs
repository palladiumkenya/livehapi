using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IProvider
    {
        string Initials { get; set; }
        string Code { get; set; }
        string ProviderTypeId { get; set; }
        int? Phone { get; set; }
        string Email { get; set; }
    }
}