﻿namespace LiveHAPI.Core.Interfaces.Model
{
    public interface IUser
    {
        string UserName { get; set; }
        string Password { get; set; }
        int? Phone { get; set; }
        string Email { get; set; }
        string Source { get; set; }
        string SourceRef { get; set; }
        string SourceSys { get; set; }
    }
}