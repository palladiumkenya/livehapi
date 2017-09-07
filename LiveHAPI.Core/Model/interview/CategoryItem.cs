using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class CategoryItem : Entity<Guid>
    {
        
        public Guid CategoryId { get; set; }
        
        public Guid ItemId { get; set; }
        
        public Item Item { get; set; }
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