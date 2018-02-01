using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.IQCare.Core.Model
{
    [Table("mst_User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserLastName { get; set; }
        public string UserFirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? EmployeeId { get; set; }
        public int? DeleteFlag { get; set; }
       

        public User()
        {
        }

        public User(string userName)
        {
            UserLastName = userName;
            UserFirstName = userName;
            UserName = userName;
            Password = "QWM9jCr6w8w=";
            DeleteFlag = 0;
        }

        public override string ToString()
        {
            return $"{UserName} ({UserLastName} {UserFirstName})";
        }
    }
}
