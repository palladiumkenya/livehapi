using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveHAPI.IQCare.Core.Model
{
    // ReSharper disable once InconsistentNaming

    [Table("mAfyaView")]
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Sex { get; set; }
        public DateTime Dob { get; set; }
        public int? DobPrecision { get; set; }
        public string HTSID { get; set; }
        public int LocationId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int DeleteFlag { get; set; }
        public Guid? mAfyaId { get; set; }



        public override string ToString()
        {
            return $"{FirstName} {MiddleName} {LastName}";
        }
        public string ToStringDetail()
        {
            return $"{HTSID}|{FirstName} {MiddleName} {LastName}|{mAfyaId??new Guid()}";
        }
    }
}

/*
Ptn_Pk
CAST(decryptbykey(FirstName) AS varchar(50))
CAST(decryptbykey(LastName) AS varchar(50))
CAST(decryptbykey(MiddleName) AS varchar(50))
LocationID
RegistrationDate
DOB
Sex
DobPrecision
Client_Code
UserID
CreateDate
UpdateDate
DeleteFlag
*/
