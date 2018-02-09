using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LiveHAPI.Core.Model.Lookup;
using LiveHAPI.Shared.Custom;
using LiveHAPI.Shared.Interfaces.Model;
using LiveHAPI.Shared.Model;
using LiveHAPI.Shared.ValueObject;

namespace LiveHAPI.Core.Model.People
{
    public class Person:Entity<Guid>, IPerson
    {
        [MaxLength(10)]
        public  string Gender { get; set; }
        public  DateTime? BirthDate { get; set; }
        public  bool? BirthDateEstimated { get; set; }

        public ICollection<PersonName> Names { get; set; } = new List<PersonName>();
        public  ICollection<PersonAddress> Addresses { get; set; }=new List<PersonAddress>();        
        public  ICollection<PersonContact> Contacts { get; set; }=new List<PersonContact>();
        public  ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Provider> Providers { get; set; }=new List<Provider>();        
        public ICollection<Client> Clients { get; set; } = new List<Client>();

        public Person()
        {
            Id = LiveGuid.NewGuid();
        }
        private Person(Guid id,string gender, DateTime? birthDate, bool? birthDateEstimated): base(id)
        {
            Gender = gender;
            BirthDate = birthDate;
            BirthDateEstimated = birthDateEstimated;
        }
        private Person(string gender, DateTime? birthDate, bool? birthDateEstimated) : this(LiveGuid.NewGuid(),gender,birthDate,birthDateEstimated)
        {
        }
        public static Person CreateClient(PersonInfo personInfo)
        {
            var person = new Person(personInfo.Id, personInfo.Gender, personInfo.BirthDate, personInfo.BirthDateEstimated);

            var personNames = PersonName.Create(personInfo);
            person.AddNames(personNames);

            var addresses = PersonAddress.Create(personInfo);
            person.AddAddresss(addresses);

            var contacts = PersonContact.Create(personInfo);
            person.AddContacts(contacts);

            return person;
        }
        public void UpdateClient(PersonInfo personInfo)
        {
            Gender = personInfo.Gender;
            BirthDate = personInfo.BirthDate;
            BirthDateEstimated = personInfo.BirthDateEstimated;

            var personNames = PersonName.Create(personInfo);
            Names.Clear();
            AddNames(personNames);

            var addresses = PersonAddress.Create(personInfo);
            Addresses.Clear();
            AddAddresss(addresses);

            var contacts = PersonContact.Create(personInfo);
            Contacts.Clear();
            AddContacts(contacts);
        }

        public static Person CreateUser(UserInfo userInfo)
        {
            var person = new Person();
            var personName = PersonName.Create(userInfo.PersonNameInfo);
            person.AddName(personName);
            return person;
        }
        public static Person CreateProvider(ProviderInfo providerInfo)
        {
            var person = new Person();
            var personName = PersonName.Create(providerInfo.PersonNameInfo);
            person.AddName(personName);
            return person;
        }
        public PersonName AssignName(PersonName name)
        {
            name.PersonId = Id;

            var personName = Names
                .FirstOrDefault(x => x.Source.IsSameAs(name.Source) &&
                                     x.SourceRef.IsSameAs(name.SourceRef) &&
                                     x.SourceSys.IsSameAs(name.SourceSys));
            if (null != personName)
            {
                Names.Remove(personName);
                personName.ChangeTo(name);
                Names.Add(personName);
                return personName;
            }

            Names.Add(name);
            return name;
        }

        public PersonAddress AssignAddress(PersonAddress address)
        {
            address.PersonId = Id;


            var personAddress = Addresses.FirstOrDefault(x => x.Source.ToLower() == address.Source.ToLower() &&
                                                     x.SourceRef.ToLower() == address.SourceRef.ToLower());

            if (null != personAddress)
            {
                Addresses.Remove(personAddress);
                personAddress.ChangeTo(address);
                Addresses.Add(personAddress);
                return personAddress;
            }

            Addresses.Add(address);
            return address;

        }

        public PersonContact AssignContact(PersonContact contact)
        {
            contact.PersonId = Id;

            var personContact = Contacts.FirstOrDefault(x => x.Source.ToLower() == contact.Source.ToLower() &&
                                                    x.SourceRef.ToLower() == contact.SourceRef.ToLower());



            if (null != personContact)
            {
                Contacts.Remove(personContact);
                personContact.ChangeTo(contact);
                Contacts.Add(personContact);
                return personContact;
            }

            Contacts.Add(contact);
            return contact;
        }

        public User AssignUser(User user)
        {
            if (null == user)
                throw new ArgumentException("No user!");

            user.PersonId = Id;

            var personUser = Users
                .FirstOrDefault(
                    x => x.Source.IsSameAs(user.Source) &&
                         x.SourceRef.IsSameAs(user.SourceRef) &&
                         x.SourceSys.IsSameAs(user.SourceSys));
            if (null != personUser)
            {
                Users.Remove(personUser);
                personUser.ChangeTo(user);
                Users.Add(personUser);
                return personUser;
            }
            
            Users.Add(user);
            return user;
        }
        //Provider
        public Provider AssignProvider(Provider provider)
        {
            if (null == provider)
                throw new ArgumentException("No Provider!");

            provider.PersonId = Id;

            var personProvider = Providers
                .FirstOrDefault(
                    x => x.Source.IsSameAs(provider.Source) &&
                         x.SourceRef.IsSameAs(provider.SourceRef) &&
                         x.SourceSys.IsSameAs(provider.SourceSys));

            if (null != personProvider)
            {
                Providers.Remove(personProvider);
                personProvider.ChangeTo(provider);
                Providers.Add(personProvider);
                return personProvider;
            }

            Providers.Add(provider);
            return provider;
        }


        private void AddNames(List<PersonName> names)
        {
            foreach (var personName in names)
            {
                AddName(personName);
            }
        }
        public void AddName(PersonName name)
        {
            name.PersonId = Id;
            Names.Add(name);
        }

        public void AddProvider(Provider name)
        {
            name.PersonId = Id;
            Providers.Add(name);
        }


        private void AddAddresss(List<PersonAddress> addresses)
        {
            foreach (var address in addresses)
            {
                AddAddress(address);
            }
        }
        private void AddAddress(PersonAddress address)
        {
            address.PersonId = Id;
            Addresses.Add(address);
        }

        private void AddContacts(List<PersonContact> contacts)
        {
            foreach (var contact in contacts)
            {
                AddContact(contact);
            }
        }
        private void AddContact(PersonContact contact)
        {
            contact.PersonId = Id;
            Contacts.Add(contact);
        }
        public bool MatchScore(Person otherPerson)
        {
            return Gender == otherPerson.Gender && 
                BirthDate.Value.Date == otherPerson.BirthDate.Value;
        }

        public PersonInfo GetPersonInfo()
        {
            var p= new PersonInfo();
            p.Id = Id;
            p.Gender = Gender;
            p.BirthDate = BirthDate;
            p.BirthDateEstimated = BirthDateEstimated;
            p.FirstName = Names.FirstOrDefault()?.FirstName;
            p.MiddleName = Names.FirstOrDefault()?.MiddleName;
            p.LastName = Names.FirstOrDefault()?.LastName;
            p.MothersName = Names.FirstOrDefault()?.MothersName;
            p.Addresses = PersonAddress.GetAddressInfos(Addresses.ToList());
            p.Contacts = PersonContact.GetContactInfos(Contacts.ToList());
            return p;
        }

        public ClientInfo GetClientInfo()
        {
            var c=new ClientInfo();
            var cl = Clients.FirstOrDefault();
            if (null != cl)
            {
                c.Id = cl.Id;
                c.MaritalStatus = cl.MaritalStatus;
                c.KeyPop = cl.KeyPop;
                c.OtherKeyPop = cl.OtherKeyPop;
                c.IsFamilyMember = cl.IsFamilyMember;
                c.IsPartner = cl.IsPartner;
                c.PracticeId = cl.PracticeId;
                c.Person = GetPersonInfo();
                if (null != c.Person)
                {
                    c.PersonId = c.Person.Id;
                }
                c.Identifiers = ClientIdentifier.GetIdentifierInfos(cl.Identifiers.ToList());
                c.Relationships= ClientRelationship.GetClientRelationshipInfos(cl.Relationships.ToList());
            }

            return c;
        }

        public bool HasGender()
        {
            return !string.IsNullOrWhiteSpace(Gender);
        }
        public bool HasDOB()
        {
            return null != BirthDate && BirthDate.HasValue;
        }
        public bool HasDOBEstimate()
        {
            return null != BirthDateEstimated && BirthDateEstimated.HasValue;
        }
        public bool ProfileNeedsUpdate()
        {
            return !HasGender() || !HasDOB()||!HasDOBEstimate();
        }
        public override string ToString()
        {
            var info = $" {Gender}|{BirthDate:yyyy MMMM dd}";
            var names = Names.Count > 0 ? Names.First().FullName : "";
            return $"{names}{info}      ({Id})";
        }
    }
}