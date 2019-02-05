using System;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject.Meta
{
    public class CategoryItemInfo :  ICategoryItem
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ItemId { get; set; }
        public string Display { get; set; }
        public Decimal Rank { get; set; }
        public bool Voided { get; set; }
    }
}