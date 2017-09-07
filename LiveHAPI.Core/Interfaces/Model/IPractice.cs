using System;

namespace LiveHAPI.Core.Interfaces.Model
{
    public interface IPractice
    {
        string Code { get; set; }
        string Name { get; set; }
        Guid Id { get; set; }
    }
}