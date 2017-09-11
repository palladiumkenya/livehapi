using System;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject
{
    public class ContactInfo : IContact
    {
        public int Phone { get; set; }
    }
}