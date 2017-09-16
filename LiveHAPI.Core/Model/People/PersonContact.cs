using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.People
{
    public class PersonContact : Entity<Guid>, IContact, ISourceIdentity
    {
        public int Phone { get; set; }
        [MaxLength(50)]
        public string Source { get; set; }
        [MaxLength(50)]
        public string SourceRef { get; set; }
        [MaxLength(50)]
        public string SourceSys { get; set; }
        public bool Preferred { get; set; }
        public Guid PersonId { get; set; }
        public PersonContact()
        {
            Id = LiveGuid.NewGuid();
        }

        public void ChangeTo(PersonContact contact)
        {
            Phone = contact.Phone;
        }

        public PersonContact(int phone):this()
        {
            Phone = phone;
        }

        public static PersonContact Create(ContactInfo address)
        {
            return new PersonContact(address.Phone);
        }

        public static List<PersonContact> Create(PersonInfo personInfo)
        {
            var list = new List<PersonContact>();

            foreach (var address in personInfo.Contacts)
            {
                list.Add(Create(address));
            }
            return list;
        }

        public override string ToString()
        {
            return $"{Phone}";
        }

        public static List<ContactInfo> GetContactInfos(List<PersonContact> contacts)
        {
            var list = new List<ContactInfo>();
            foreach (var contact in contacts)
            {
                list.Add(contact.GetContactInfo());
            }

            return list;
        }

        private ContactInfo GetContactInfo()
        {
            return new ContactInfo(Id,Phone,PersonId);
        }
    } 
}