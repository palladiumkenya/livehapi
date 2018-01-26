using System;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.People
{
    public class User:Entity<Guid>, IUser,ISourceIdentity
    {
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(200)]
        public string Password { get; set; }
        public int? Phone { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Source { get; set; }
        [MaxLength(50)]
        public string SourceRef { get; set; }
        [MaxLength(50)]
        public string SourceSys { get; set; }
        public Guid? PracticeId { get; set; }        
        public Guid PersonId { get; set; }

        public User()
        {
            Id = LiveGuid.NewGuid();
        }

        private User(string userName, string password, int? phone, string email):this()
        {
            UserName = userName;
            Password = password;
            Phone = phone;
            Email = email;
        }

        private User(string userName, string password, int? phone, string email, string source, string sourceRef, string sourceSys, Guid practiceId) :this( userName,  password,  phone,  email)
        {
            Source = source;
            SourceRef = sourceRef;
            SourceSys = sourceSys;
            PracticeId = practiceId;
        }
        public static User Create(UserInfo userInfo,Guid practiceId)
        {
            return new User(userInfo.UserName,userInfo.Password,userInfo.Phone,userInfo.Email, userInfo.SourceIdentity.Source, userInfo.SourceIdentity.SourceRef, userInfo.SourceIdentity.SourceSys,practiceId);
        }
        public void ChangeTo(User name)
        {
            UserName = name.UserName;
            Password = name.Password;
            Phone = name.Phone;
            Email = name.Email;
        }

        public void UpdateTo(User name)
        {
            UserName = name.UserName;
            Password = name.Password;
            SourceRef = name.SourceRef;
        }

        public override string ToString()
        {
            return $"{UserName} ({SourceSys})";
        }
    }
}