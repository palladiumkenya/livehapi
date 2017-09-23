using System;

namespace LiveHAPI.Shared.Interfaces.Model
{
    public interface ICategoryItem
    {
        Guid CategoryId { get; set; }
        Guid ItemId { get; set; }
        string Display { get; set; }
        Decimal Rank { get; set; }
        
    }
}