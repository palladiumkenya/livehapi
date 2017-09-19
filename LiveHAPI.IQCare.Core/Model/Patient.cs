using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LiveHAPI.Core.Model.People;
using LiveHAPI.Shared.ValueObject;

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
        public string Phone { get; set; }
        public int Sex { get; set; }
        public DateTime Dob { get; set; }
        public int? DobPrecision { get; set; }
        
        public string HTSID { get; set; }
        public int LocationId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int DeleteFlag { get; set; }
        public int UserId { get; set; }
        public Guid? mAfyaId { get; set; }

        private Patient(string firstName, string middleName, string lastName, int sex, DateTime dob, int? dobPrecision, string htsid, int locationId, DateTime? registrationDate ,Guid? mafyaId)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Sex = sex;
            Dob = dob;
            DobPrecision = dobPrecision;
            HTSID = htsid;
            LocationId = locationId;
            RegistrationDate = registrationDate;
            CreateDate =DateTime.Now;
            mAfyaId = mafyaId;
        }

        public static Patient Create(ClientInfo client,int locationId)
        {
            return new Patient(
                client.Person.FirstName,
                client.Person.MiddleName,
                client.Person.LastName,
                GetSex(client.Person.Gender),
                client.Person.BirthDate.Value,
                GetDobPrecion(client.Person.BirthDateEstimated.Value),
                client.Identifiers.First().Identifier,
                locationId,
                client.Identifiers.First().RegistrationDate,
                client.Id);
        }

        public static int GetSex(string gender)
        {
            return gender == "M" ? 16 : 17;
        }

        public static int GetDobPrecion(bool estimated)
        {
            return estimated? 1 : 0;
        }

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
