using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IPersonName
    {
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string MothersName { get; set; }
        string NickName { get; set; }
    }
}