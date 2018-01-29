using System;

namespace LiveHAPI.Shared.ValueObject
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid? PracticeId { get; set; }
        public Guid PersonId { get; set; }
        public int? UserId { get; set; }
        public ProviderDTO Provider { get; set; }
        public PersonDTO Person { get; set; }
    }
}