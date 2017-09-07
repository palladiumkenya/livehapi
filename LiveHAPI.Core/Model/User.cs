using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model
{
    public class User:Entity<Guid>, IUser
    {
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(200)]
        public string Password { get; set; }
        
        public Guid? PracticeId { get; set; }
        
        public Guid PersonId { get; set; }
        
        
        public User()
        {
            Id = LiveGuid.NewGuid();
        }

        public override string ToString()
        {
            return $"{UserName}";
        }
    }
}