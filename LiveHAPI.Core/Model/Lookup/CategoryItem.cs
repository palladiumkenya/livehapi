using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Lookup
{
    public class CategoryItem : Entity<Guid>, ICategoryItem
    {
        public Guid CategoryId { get; set; }
        public Guid ItemId { get; set; }
        [MaxLength(100)]
        public string Display { get; set; }
        public Decimal Rank { get; set; }

        public CategoryItem()
        {
            Id = LiveGuid.NewGuid();
        }
        public override string ToString()
        {
            return Display;
        }
    }
}