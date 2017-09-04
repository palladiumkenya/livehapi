using System;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class User:Entity<Guid>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        
        public Guid? PracticeId { get; set; }
        
        public Guid PersonId { get; set; }
        
        public Person Person { get; set; }
        public User()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Person}";
        }
    }
}