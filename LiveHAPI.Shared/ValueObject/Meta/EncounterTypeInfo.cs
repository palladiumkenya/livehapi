using System;
using LiveHAPI.Shared.Interfaces.Model;

namespace LiveHAPI.Shared.ValueObject.Meta
{
    public class EncounterTypeInfo : IEncounterType
    {        
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}