using System;
using System.Collections.Generic;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class Category : Entity<Guid>
    {
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