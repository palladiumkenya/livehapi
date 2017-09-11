using System;

namespace LiveHAPI.Core.Model.Lookup
{
    public interface IItem
    {
        string Code { get; set; }
        string Display { get; set; }
        
    }
}