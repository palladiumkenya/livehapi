using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.People
{
    public class PersonName:Entity<Guid>, IPersonName
    {
        [MaxLength(100)]
        public  string FirstName { get; set; }
        [MaxLength(100)]
        public  string MiddleName { get; set; }
        [MaxLength(100)]
        public  string LastName { get; set; }
        [MaxLength(100)]
        public string MothersName { get; set; }
        [MaxLength(50)]
        public string Source { get; set; }
        [MaxLength(50)]
        public string SourceRef { get; set; }
        [MaxLength(50)]
        public string SourceSys { get; set; }
        public bool Preferred { get; set; }
        public Guid PersonId { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {MiddleName} {LastName}"; }
        }

        public PersonName()
        {
            Id = LiveGuid.NewGuid();
        }
        private PersonName(string firstName, string middleName, string lastName, string mothersName, string source, string sourceRef, string sourceSys):this()
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            MothersName = mothersName;
            Source = source;
            SourceRef = sourceRef;
            SourceSys = sourceSys;
        }
        public static PersonName Create(PersonInfo personInfo)
        {
            return new PersonName(personInfo.FirstName, personInfo.MiddleName, personInfo.LastName, personInfo.MothersName, personInfo.Identity.Source,
                personInfo.Identity.SourceRef, personInfo.Identity.SourceSys);
        }

        public void ChangeTo(PersonName name)
        {
            FirstName = name.FirstName;
            MiddleName = name.MiddleName;
            LastName = name.LastName;
            MothersName = name.MothersName;
        }

        public override string ToString()
        {
            return $"{FullName}";
        }
    }
}