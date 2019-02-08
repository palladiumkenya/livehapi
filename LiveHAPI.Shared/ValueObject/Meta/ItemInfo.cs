using System;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject.Meta
{
    public class ItemInfo :  IItem
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Display { get; set; }
        public bool Voided { get; set; }
    }
}