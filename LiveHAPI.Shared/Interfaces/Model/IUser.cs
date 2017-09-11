using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface IUser
    {
        
        string UserName { get; set; }
        string Password { get; set; }
        int? Phone { get; set; }
        string Email { get; set; }
    }
}