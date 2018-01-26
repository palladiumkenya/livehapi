using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using LiveHAPI.Shared.Custom;

namespace LiveHAPI.IQCare.Core.Model
{
    [Table("mst_User")]
    public class User
    {
        public int UserId { get; set; }
        public string UserLastName { get; set; }
        public string UserFirstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? DeleteFlag { get; set; }
        [NotMapped]
        public string DecryptedPassword => Utils.Decrypt(Password);

        public override string ToString()
        {
            return $"{UserName} ({UserLastName} {UserFirstName})";
        }
    }
}
