using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Category : Entity<Guid>
    {
        [MaxLength(50)]
        public string Code { get; set; }
        
        public List<CategoryItem> Items { get; set; } = new List<CategoryItem>();

        public Category()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return Code;
        }
    }
}