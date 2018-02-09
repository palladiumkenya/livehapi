using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveHAPI.IQCare.Core.Model
{
    [Table("mst_groups")]
    public class Group
    {
        [Key]
        public int GroupID { get; set; }
        public string GroupName  { get; set; }
        public int? DeleteFlag { get; set; } 
        public int? UserID { get; set; } 
        public DateTime? CreateDate { get; set; } 
        public DateTime? UpdateDate { get; set; } 
        public int? EnrollmentFlag  { get; set; } 
        public int? CareEndFlag { get; set; } 
        public int? IdentifierFlag { get; set; }

        public Group()
        {
        }

        public Group(string groupName)
        {
            GroupName = groupName;
            DeleteFlag = 0;
            UserID = 1;
            CreateDate=DateTime.Now;
            EnrollmentFlag = 0;
            CareEndFlag = 1;
            IdentifierFlag = 1;
        }

        public override string ToString()
        {
            return $"{GroupID} - {GroupName}";
        }

        public void UpdateTo(Group @group)
        {
            GroupName = @group.GroupName;
            UpdateDate=DateTime.Now;
        }
    }
}