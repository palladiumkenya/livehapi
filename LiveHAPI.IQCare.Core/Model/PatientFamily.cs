using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveHAPI.IQCare.Core.Model
{
    [Table("mAfyaFamilyView")]
    public class PatientFamily
    {
        [Key]
        public int Id { get; set; }
        public int Ptn_pk { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Sex { get; set; }
        public int AgeYear { get; set; }
        public int AgeMonth { get; set; }
        public DateTime RelationshipDate { get; set; }
        public int RelationshipType { get; set; }
        public int HivStatus { get; set; }
        public int HivCareStatus { get; set; }
        public string RegistrationNo { get; set; }
        public string FileNo { get; set; }
        public int ReferenceId { get; set; }
        public int UserId { get; set; }
        public int DeleteFlag { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Ptn_pk})";
        }
    }
}
