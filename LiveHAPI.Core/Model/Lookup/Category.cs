using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Model.Studio;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.Lookup
{
    public class Category : Entity<Guid>, ICategory
    {
        [MaxLength(50)]
        public string Code { get; set; }
        
        public ICollection<CategoryItem> Items { get; set; } = new List<CategoryItem>();
        public ICollection<Concept> Concepts { get; set; } = new List<Concept>();
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