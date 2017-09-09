using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Core.Interfaces.Model;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Model;

namespace LiveHAPI.Core.Model.People
{
    public class Person:Entity<Guid>, IPerson
    {
        [MaxLength(100)]
        public  string FirstName { get; set; }
        [MaxLength(100)]
        public  string MiddleName { get; set; }
        [MaxLength(100)]
        public  string LastName { get; set; }
        [MaxLength(10)]
        public  string Gender { get; set; }
        public  DateTime BirthDate { get; set; }
        public  bool? BirthDateEstimated { get; set; }
        [MaxLength(100)]
        public  string Email { get; set; }
        public virtual string FullName
        {
            get { return $"{FirstName} {MiddleName} {LastName}"; }
        }
        public  ICollection<PersonAddress> Addresses { get; set; }=new List<PersonAddress>();
        
        public  ICollection<PersonContact> Contacts { get; set; }=new List<PersonContact>();

        public  ICollection<User> Users { get; set; } = new List<User>();

        public ICollection<Provider> Providers { get; set; }=new List<Provider>();

        
        public ICollection<Client> Clients { get; set; } = new List<Client>();


        public Person()
        {
            Id = LiveGuid.NewGuid();
        }

        private Person(string firstName, string middleName, string lastName, string gender, DateTime birthDate, bool? birthDateEstimated=false, string email="")
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            BirthDateEstimated = birthDateEstimated;
            Email = email;
        }

        public void AssignUser(User user)
        {
            
        }
        public override string ToString()
        {
            return $"{FullName}";
        }
    }
}