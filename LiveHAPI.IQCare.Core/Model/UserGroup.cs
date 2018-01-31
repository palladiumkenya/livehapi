using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveHAPI.IQCare.Core.Model
{
    [Table("lnk_usergroup")]
    public class UserGroup
    {
        public int GroupID { get; set; }
        public int UserID { get; set; }
        public int? OperatorID { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? DeleteFlag { get; set; }

        public UserGroup()
        {
        }

        private UserGroup(int groupId, int userId)
        {
            GroupID = groupId;
            UserID = userId;
            OperatorID = userId;
            CreateDate=DateTime.Now;
            DeleteFlag = 0;
        }

        public override string ToString()
        {
            return $"{GroupID} - {UserID} - {OperatorID}";
        }

        public static List<UserGroup> Create(int groupId, List<int> userIds)
        {
            var userGroups = new List<UserGroup>();

            foreach (var userId in userIds)
            {
               userGroups.Add(new UserGroup(groupId,userId));
            }

            return userGroups;
        }
    }
}