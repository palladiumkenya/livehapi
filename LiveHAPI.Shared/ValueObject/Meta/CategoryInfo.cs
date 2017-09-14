using System;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject.Meta
{
    public class CategoryInfo: ICategory
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
    }
}